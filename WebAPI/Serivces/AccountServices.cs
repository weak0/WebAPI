using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public AccountServices(RestaurantDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IValidator<AccountDto> accountValidator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _accountValidator = accountValidator;
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
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes())
            return "tomek chuj";
        } 
    }
}
