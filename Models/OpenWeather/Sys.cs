using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.OpenWeather
{
    public class Sys
    {
        [JsonPropertyName("pod")]
        public string? Pod { get; set; }
    }
}

