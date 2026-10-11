using SecondApp;
using System;
using System.Text.Json;

const string FileName = "Cities.txt";
const string ApiAdress = "https://wttr.in/CITY?format=j1";
HttpClient client = new HttpClient();

await MainAsync();

async Task MainAsync()
{
    if (!File.Exists(FileName))
    {
        Console.WriteLine($"File {FileName} doesn't exist");
        return;
    }

    var tasks = File.ReadAllLines(FileName)
        .Select(cityName => GetData(cityName));

    var results = await Task.WhenAll(tasks); // будем надеяться и верить, что городов, как и в примере, будет пять штук
    var weatherData = results.OfType<WeatherData>().ToList();
}

async Task<WeatherData> GetData(string cityName)
{
    var api = ApiAdress.Replace("CITY", cityName);

    try
    {
        string jsonResponse = await client.GetStringAsync(api);

        var weatherData = JsonSerializer.Deserialize<WeatherInfo>(jsonResponse);

        if (weatherData?.CurrentCondition != null && weatherData.CurrentCondition.Count > 0)
        {
            var current = weatherData.CurrentCondition[0];
            var nearestArea = weatherData.NearestArea?[0];

            if (nearestArea == null || nearestArea.AreaName == null
                || nearestArea.AreaName.Count == 0 || nearestArea.Country == null
                || nearestArea.Country.Count == 0)
                return null;

            return (new(nearestArea?.AreaName?[0].Value, float.Parse(current.TempC), nearestArea?.Country?[0].Value));
        }

        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"exception at url: {ex.Message}");
        return null;
    }
}