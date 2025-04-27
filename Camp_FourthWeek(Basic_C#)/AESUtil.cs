namespace Camp_FourthWeek_Basic_C__;

using System.Security.Cryptography;
using System.Text;

/*
 * AES란?
 * 하나의 Key를 가지고 암호화와 복호화 둘 다 수행하는 방식
 */
public static class AESUtil
{
    private static readonly string key = "Goated Team 19jo"; // 16자 = 128bit
    private static readonly string iv = "FEDCBA0987654321"; // 16자 = 128bit


    public static string Encrypt(string text)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using MemoryStream memoryStream = new MemoryStream();
        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(text);
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string text)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[] bytes = Convert.FromBase64String(text);

        using MemoryStream memoryStream = new MemoryStream(bytes);
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}