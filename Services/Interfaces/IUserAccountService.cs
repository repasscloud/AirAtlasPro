using AirAtlasPro.Models.Account;

namespace AirAtlasPro.Services.Interfaces
{
	public interface IUserAccountService
	{
		User UserAccount { get; set; }

		Task LoadUserAccountAsync();
		Task<int> CreateUpdateUserAccount(User userAccount);
	}
}

