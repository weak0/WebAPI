using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Models.Validations;

namespace WebAPI.Serivces
{
    public class AccountServices : IAccountServices
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IValidator<AccountDto> _accountValidator;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountServices(RestaurantDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher,
            IValidator<AccountDto> accountValidator, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _accountValidator = accountValidator;
            _authenticationSettings = authenticationSettings;
        }

        public int CreateUser(AccountDto account)
        {
            var validationResult = _accountValidator.Validate(account);
            if (!validationResult.IsValid) {
                var errorMessages = string.Join(", ", validationResult.Errors);
                throw new Exception("Validate errors: " + errorMessages ); }
            var user = new User()
            {
                Email = account.Email,
                Name = account.Name,
                DateOfBirth = account.DateOfBirth,
                RoleId = 1


            };
            var hasshedPassword = _passwordHasher.HashPassword(user, account.Password);
            user.Password = hasshedPassword;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }
        public string GenearteToken(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or username");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid email or username");
            }

            var claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
            new Claim(ClaimTypes.Email, user.Id.ToString()),
            new Claim("DateOfBirth", user.DateOfBirth.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(
                issuer: _authenticationSettings.JwtIssuer,
                audience: _authenticationSettings.JwtIssuer,
                claims: claims,
                expires: expires,
                signingCredentials: cred
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var generatedToken = tokenHandler.WriteToken(token);

            return generatedToken;
        } 
    }
}
