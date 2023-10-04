using AirAtlasPro.Helpers;
using AirAtlasPro.Models.Account;
using AirAtlasPro.Services.Interfaces;

namespace AirAtlasPro.Services
{
    public class UserAccountService : IUserAccountService
	{
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public event Action? OnChange;

        public UserAccountService(HttpClient httpClient, IConfiguration configuration)
        {
             _httpClient = httpClient;
             _configuration = configuration;
		}

        void UserAccountChanged() => OnChange?.Invoke();

        public User UserAccount { get; set; } = new();

        public async Task LoadUserAccountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<User>("/api/user");
            if (response != null)
            {
                UserAccount = response;
                UserAccountChanged();
            }
        }

        public async Task<int> CreateUpdateUserAccount(User userAccount)
        {
            string encryptionKey = _configuration["AirAtlasPro:EncryptionKey"]!;
            string fixedIV = _configuration["AirAtlasPro:FixedIV"]!;
            int desiredLength = 16;

            // Trim to the first 16 characters and pad if necessary
            string resultFixedIV = fixedIV.Length >= desiredLength
                ? fixedIV.Substring(0, desiredLength)
                : fixedIV.PadRight(desiredLength, '0'); // Replace '0' with the character you want to pad with

            EncryptionHelper encryptionHelper = new EncryptionHelper(encryptionKey, resultFixedIV);

            string encryptedEmail = encryptionHelper.Encrypt(userAccount.EmailAddress);

            var response = await _httpClient.GetAsync($"/api/account/user/email/{encryptedEmail}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                User postUserAccount = userAccount;
                postUserAccount.EmailAddress = encryptedEmail;
                await _httpClient.PostAsJsonAsync<User>($"/api/account/user/email/{encryptedEmail}", postUserAccount);
                UserAccountChanged();
                return 0;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await _httpClient.PutAsJsonAsync($"/api/account/user/{encryptedEmail}", userAccount);
                UserAccountChanged();
                return 0;
            }

            // Everything failed
            return 1;
        }
    }
}

