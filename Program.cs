using MyWeatherApp.Services;
using MyWeatherApp.Models;

var cities = CityLoader.Load(Path.Combine(AppContext.BaseDirectory, "Cities.txt"));
WeatherClient client = new WeatherClient();
var rezults = new List<WeatherData>();

foreach(var c in cities)
{
    WeatherData? weather = await client.GetWeatherAsync(c);
    if (weather != null)
        rezults.Add(weather);
}
// По городам , прост вывод 
foreach(var rw in rezults)
{
    Console.WriteLine($"{rw.City}, {rw.Country} {MyFormat(rw.TemperatureC)} °C");
}

Console.WriteLine();

// GroupBy по странам
foreach(var g in rezults.GroupBy(w => w.Country).OrderBy(g => g.Key))
{
    var avg = Math.Round(g.Average(t => t.TemperatureC));
    var min = g.Min(t => t.TemperatureC);
    var max = g.Max(t => t.TemperatureC);
    Console.WriteLine($"{g.Key} – {g.Count()} cities, avg: {MyFormat(avg)} °C, min: {MyFormat(min)} °C, max: {MyFormat(max)} °C");
}

// Лучше форматирование отдельно вывести а то ToString везде писать... плохо...
string MyFormat(double d) => d.ToString("+0;-0;0");
