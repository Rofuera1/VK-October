using System.Text.Json.Serialization;

namespace SecondApp
{
    public class WeatherInfo
    {
        [JsonPropertyName("current_condition")]
        public List<CurrentCondition> CurrentCondition { get; set; }

        [JsonPropertyName("nearest_area")]
        public List<NearestArea> NearestArea { get; set; }
    }

    public class CurrentCondition
    {
        [JsonPropertyName("temp_C")]
        public string TempC { get; set; }

        [JsonPropertyName("FeelsLikeC")]
        public string FeelsLikeC { get; set; }

        [JsonPropertyName("humidity")]
        public string Humidity { get; set; }

        [JsonPropertyName("pressure")]
        public string Pressure { get; set; }

        [JsonPropertyName("windspeedKmph")]
        public string WindSpeedKmh { get; set; }

        [JsonPropertyName("weatherDesc")]
        public List<ValueWrapper> WeatherDesc { get; set; }
    }

    public class NearestArea
    {
        [JsonPropertyName("areaName")]
        public List<ValueWrapper> AreaName { get; set; }

        [JsonPropertyName("country")]
        public List<ValueWrapper> Country { get; set; }
    }

    public class ValueWrapper
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
