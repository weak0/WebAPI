using NLog;
using NLog.Web;
using WebAPI;
using WebAPI.Entities;
using WebAPI.Middleware.cs;
using WebAPI.Serivces;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<RestaurantDbContext>();
    builder.Services.AddScoped<RestaurantSeeder>();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddScoped<IRestauranServices, RestauranServices>();
    builder.Services.AddScoped<ErrorHandlingMiddlewarecs>();
    builder.Services.AddScoped<RequestTimeMiddleware>();
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
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
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




