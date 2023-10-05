using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.OpenWeather
{
    // Forecast weatherForecast = JsonSerializer.Deserialize<Forecast>(myJsonResponse);
    public class Forecast
    {
        [JsonPropertyName("cod")]
        public string Cod { get; set; } = null!;

        [JsonPropertyName("message")]
        public int Message { get; set; }

        [JsonPropertyName("cnt")]
        public int Cnt { get; set; }

        [JsonPropertyName("list")]
        public List<List>? List { get; set; }

        [JsonPropertyName("city")]
        public City? City { get; set; }
    }
}
