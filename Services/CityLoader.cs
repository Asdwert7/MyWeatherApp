namespace MyWeatherApp.Services;

public static class CityLoader
{
    public static List<string> Load(string lpath) =>
    File.ReadAllLines(lpath)
    .Select(line => line.Trim())
    .Where(city => city.Length > 0)
    .Distinct(StringComparer.OrdinalIgnoreCase)
    .ToList();

}