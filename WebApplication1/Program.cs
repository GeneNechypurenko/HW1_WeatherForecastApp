using HW1_WeatherForecastApp.Services;

namespace HW1_WeatherForecastApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<WeatherService>();

            var apiKey = builder.Configuration["OpenWeatherMap:ApiKey"];
            builder.Services.AddSingleton(new Config { ApiKey = apiKey });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapPost("/get-weather", async context =>
            {
                var city = context.Request.Form["city"];
                var weatherService = context.RequestServices.GetRequiredService<WeatherService>();
                var weatherData = await weatherService.GetCurrentWeatherAsync(city);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(weatherData);
            });

            app.MapPost("/get-hourly-forecast", async context =>
            {
                var city = context.Request.Form["city"];
                var weatherService = context.RequestServices.GetRequiredService<WeatherService>();
                var forecastData = await weatherService.GetHourlyForecastAsync(city);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(forecastData);
            });

            app.Run();
        }
    }
}
