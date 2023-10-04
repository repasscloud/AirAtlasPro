﻿using AirAtlasPro.Models.Constants;

namespace AirAtlasPro.Models.Account
{
	public class User
	{
		public int Id { get; set; }

		public string EmailAddress { get; set; } = null!;

		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
		public BillingType BillingType { get; set; } = 0;
    }
}

