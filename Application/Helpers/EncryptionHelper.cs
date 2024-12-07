using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers
{
    public static class EncryptionHelper
    {
        public static string EncryptString(string plainText)
        {
            string base64Key = "E3ABZR5G61T7Z884mb0jvsZ0czqhssSWDDKG5Og3mPI=";
            byte[] keyBytes = Convert.FromBase64String(base64Key);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = new byte[16];
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            if (cipherText is null)
                return "false";

            string base64Key = "E3ABZR5G61T7Z884mb0jvsZ0czqhssSWDDKG5Og3mPI=";
            byte[] keyBytes = Convert.FromBase64String(base64Key);
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }

}
