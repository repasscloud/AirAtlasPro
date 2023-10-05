using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.OpenWeather
{
    public class Coord
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }
}

