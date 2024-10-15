using System;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global_Classes
{

   
    public class ClsCrypto
    {

        // Compute Hash fanc
        public static string EcryptByHash(string input)
       {
            // SHA is Securted Hash Alorithm
            // Create an Instance of the SHA-250 Alorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                //Compute The Hash value From the UTF-8 encorded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the Byte array to a lowercase hexadecimal string 
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            }

       }

        //----------------------------[Semetric]-------------------------

        // Semetric Encryption Func
        public static string SemetricEncrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);

                /*
                Here, you are setting the IV of the AES algorithm to a block of bytes 
                with a size equal to the block size of the algorithm divided by 8. 
                The block size of AES is typically 128 bits (16 bytes), 
                so the IV size is 128 bits / 8 = 16 bytes.
                 */
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Semetric Deccryption Func
        public static string SemetricDecrypt(string cipherText, string key)
        {

            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    // Read the decrypted data from the StreamReader
                    return srDecrypt.ReadToEnd();
                }
            }
        }


    }



}
