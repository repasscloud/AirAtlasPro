﻿using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.Stripe
{
    public partial class ProrationDetails
    {
        [JsonPropertyName("credited_items")]
        public object? CreditedItems { get; set; }
    }
}

