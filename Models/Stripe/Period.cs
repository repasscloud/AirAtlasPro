using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.Stripe
{
    public partial class Period
    {
        [JsonPropertyName("end")]
        public int? End { get; set; }

        [JsonPropertyName("start")]
        public int? Start { get; set; }
    }
}

