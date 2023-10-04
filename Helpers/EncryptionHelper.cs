using System.Security.Cryptography;
using System.Text;

namespace AirAtlasPro.Helpers
{
	public class EncryptionHelper
    {
        private readonly string encryptionKey; // Your secret encryption key.
        private readonly byte[] fixedIV; // Fixed IV for encryption and decryption.

        public EncryptionHelper(string encryptionKey, string fixedIV)
        {
            this.encryptionKey = encryptionKey;
            this.fixedIV = Encoding.UTF8.GetBytes(fixedIV.PadRight(16)); // 128-bit IV for AES-256
        }

        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Derive the key from the encryptionKey using a hash function (e.g., SHA-256)
                aesAlg.Key = DeriveKey(encryptionKey);

                // Set the fixed IV
                aesAlg.IV = fixedIV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Derive the key from the encryptionKey using a hash function (e.g., SHA-256)
                aesAlg.Key = DeriveKey(encryptionKey);

                // Set the fixed IV
                aesAlg.IV = fixedIV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private byte[] DeriveKey(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }
    }
}

