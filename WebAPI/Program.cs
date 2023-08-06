using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using WebAPI;
using WebAPI.Authorization;
using WebAPI.Entities;
using WebAPI.Middleware.cs;
using WebAPI.Models;
using WebAPI.Models.Validations;
using WebAPI.Serivces;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var authetiactionSettings = new AuthenticationSettings();

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.GetSection("Authentication").Bind(authetiactionSettings);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";

    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authetiactionSettings.JwtIssuer,
            ValidAudience = authetiactionSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authetiactionSettings.JwtKey)),
        };
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Atleast18", builder => builder.Requirements.Add(new MinimumAgeRequirments(18)));
    });
    builder.Services.AddControllers();
    builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirmentHandler>();
    builder.Services.AddDbContext<RestaurantDbContext>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<RestaurantSeeder>();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddScoped<IRestauranServices, RestauranServices>();
    builder.Services.AddScoped<IAccountServices, AccountServices>();
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    builder.Services.AddScoped<IValidator<AccountDto>, AccountDtoValidation>();
    builder.Services.AddScoped<ErrorHandlingMiddlewarecs>();
    builder.Services.AddScoped<RequestTimeMiddleware>();
    builder.Services.AddSingleton(authetiactionSettings);
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var restaurantSeeder = serviceProvider.GetRequiredService<RestaurantSeeder>();
            restaurantSeeder.Seed();
        }

        app.UseMiddleware<ErrorHandlingMiddlewarecs>();
        app.UseMiddleware<RequestTimeMiddleware>();
        app.UseAuthentication();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}
catch (Exception exepction)
{
    logger.Error(exepction, "Stopped program becouse of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}




