using AirAtlasPro.Models.Stripe;
using Microsoft.EntityFrameworkCore;

namespace AirAtlasPro.Data
{
    public class ApiDbContext : DbContext
	{
		// Stripe
        public DbSet<StripeBgTaskQueue>? StripeBgTaskQueues { get; set; }


		public ApiDbContext()
		{
		}
	}
}

