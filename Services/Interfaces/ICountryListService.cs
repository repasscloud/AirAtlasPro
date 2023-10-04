namespace AirAtlasPro.Services.Interfaces
{
	public interface ICountryListService
	{
		IList<string> Countries { get; }
		Task LoadCountryListAsync();
	}
}

