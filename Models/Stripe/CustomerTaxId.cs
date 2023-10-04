using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.Stripe
{
    public partial class CustomerTaxId
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}

