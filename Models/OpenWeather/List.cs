using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.OpenWeather
{
    public class List
    {
        [JsonPropertyName("dt")]
        public int Dt { get; set; }

        [JsonPropertyName("main")]
        public Main? Main { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather>? Weather { get; set; }

        [JsonPropertyName("clouds")]
        public Clouds? Clouds { get; set; }

        [JsonPropertyName("wind")]
        public Wind? Wind { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        [JsonPropertyName("pop")]
        public int Pop { get; set; }

        [JsonPropertyName("sys")]
        public Sys? Sys { get; set; }

        [JsonPropertyName("dt_txt")]
        public string? DtTxt { get; set; }
    }
}

