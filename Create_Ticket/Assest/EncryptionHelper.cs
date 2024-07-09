using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    private static readonly string EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

    public static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.Key = key;
            aes.IV = new byte[16]; // Initialize to zero

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32));
            aes.Key = key;
            aes.IV = new byte[16]; // Initialize to zero

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}