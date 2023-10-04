using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.Stripe
{
    public partial class InvoiceData
    {
        [JsonPropertyName("object")]
        public DataObject? ObjectData { get; set; }
    }
}

