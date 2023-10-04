﻿using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.Stripe
{
    public partial class Verification
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("verified_address")]
        public string? VerifiedAddress { get; set; }

        [JsonPropertyName("verified_name")]
        public string? VerifiedName { get; set; }
    }
}

