using AirAtlasPro.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace AirAtlasPro.Models.Stripe
{
    public class StripeBgTaskQueue
    {
        [Key]
        public int Id { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionExpireDate { get; set; }

        public string? EventId { get; set; }
        public string? ProductId { get; set; }
        public string? CustomerId { get; set; }
        public string? BillingReason { get; set; }

        public StripeBgTaskStatus? Status { get; set; }
    }
}

