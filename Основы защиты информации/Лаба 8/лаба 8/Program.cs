using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    public static void Main()
    {
        string originalData = "Велютич";
        byte[] originalBytes = Encoding.UTF8.GetBytes(originalData);
        byte[] hash; // Переменная для хранения хеша

        // Step 1: AES Encryption and Saving Keys and Encrypted Data
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 192;
            aesAlg.GenerateKey();
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor();

            byte[] encryptedBytes;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(originalBytes, 0, originalBytes.Length);
                }
                encryptedBytes = msEncrypt.ToArray();
            }

            File.WriteAllBytes("aes_key.bin", aesAlg.Key);
            File.WriteAllBytes("aes_iv.bin", aesAlg.IV);
            File.WriteAllBytes("aes_encrypted.bin", encryptedBytes);
        }

        Console.WriteLine("AES Encryption completed.");

        // Step 2: AES Decryption and Verification
        byte[] key = File.ReadAllBytes("aes_key.bin");
        byte[] iv = File.ReadAllBytes("aes_iv.bin");
        byte[] encryptedData = File.ReadAllBytes("aes_encrypted.bin");

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor();

            byte[] decryptedBytes;
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream msDecrypted = new MemoryStream())
                    {
                        csDecrypt.CopyTo(msDecrypted);
                        decryptedBytes = msDecrypted.ToArray();
                    }
                }
            }

            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine("AES Decrypted Data: " + decryptedData);
        }

        Console.WriteLine("AES Decryption completed.");

        // Step 3: SHA-1 Hashing and Verification
        using (SHA1 sha1 = SHA1.Create())
        {
            hash = sha1.ComputeHash(originalBytes); // Сохраняем хеш в переменную
            File.WriteAllBytes("sha1_hash.bin", hash);
        }

        byte[] hashFromFile = File.ReadAllBytes("sha1_hash.bin");
        bool isHashValid = CompareByteArrays(hash, hashFromFile);

        Console.WriteLine("Original Data: " + originalData);
        Console.WriteLine("SHA-1 Hash Validation: " + isHashValid);
    }

    private static bool CompareByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }

        return true;
    }
}
