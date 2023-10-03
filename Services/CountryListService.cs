using AirAtlasPro.Services.Interfaces;

namespace AirAtlasPro.Services
{
	public class CountryListService : ICountryListService
	{
        private readonly HttpClient _httpClient;

		public CountryListService(HttpClient httpClient)
		{
            _httpClient = httpClient;
		}

        public IList<string> Countries { get; set; } = new List<string>();

        public async Task LoadCountryListAsync()
        {
            if (Countries.Count == 0)
            {
                var countryList = await _httpClient.GetFromJsonAsync<IList<string>>("/api/CountryList");
                if (countryList != null)
                {
                    Countries = countryList;
                }
            }
        }
    }
}

