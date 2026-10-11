using System.Configuration.Assemblies;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using MyWeatherApp.Models;

namespace MyWeatherApp.Services;

public class WeatherClient
{
    readonly HttpClient http = new()
    {
        Timeout = TimeSpan.FromSeconds(10)
    };
    public async Task<WeatherData?> GetWeatherAsync (string city)
    {
        string url = $"https://wttr.in/{Uri.EscapeDataString(city)}?format=j1";
        string jыson = await http.GetStringAsync(url);

        using JsonDocument doc = JsonDocument.Parse(jыson);

        JsonElement root = doc.RootElement;

        string temp = root.GetProperty("current_condition")[0]
                                .GetProperty("temp_C").ToString();
        JsonElement area = root.GetProperty("nearest_area")[0];
        string? name = area.GetProperty("areaName")[0]
                               .GetProperty("value")
                               .GetString();
        string? country = area.GetProperty("country")[0]
                             .GetProperty("value")
                             .GetString();
        
        return new WeatherData
        {
            City  = city,
            Country = country ?? "Хз",
            TemperatureC = int.Parse(temp!, CultureInfo.InvariantCulture)
        };

    }
}