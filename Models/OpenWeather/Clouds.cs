using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.OpenWeather
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
}

