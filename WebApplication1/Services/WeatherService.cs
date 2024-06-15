namespace HW1_WeatherForecastApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, Config config)
        {
            _httpClient = httpClient;
            _apiKey = config.ApiKey;
        }

        public async Task<string> GetCurrentWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetHourlyForecastAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
