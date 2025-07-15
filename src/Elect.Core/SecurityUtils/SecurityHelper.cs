namespace Elect.Core.SecurityUtils
{
    public class SecurityHelper
    {
        public static string GenerateSalt(int byteLength = 32)
        {
            if (byteLength <= 0)
                throw new ArgumentException("Salt length must be greater than zero.", nameof(byteLength));
            // 32 Bytes will give 256 bits.
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[byteLength];
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        // SHA
        public static string EncryptSha256(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }
        public static string EncryptSha512(string value)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }
        // HMAC SHA
        public static string EncryptHmacSha256(string value, string key)
        {
            var keyBytes = Convert.FromBase64String(key);
            var valueBytes = Encoding.UTF8.GetBytes(value);
            using (var shaAlgorithm = new HMACSHA256(keyBytes))
            {
                var hashBytes = shaAlgorithm.ComputeHash(valueBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }
        public static string EncryptHmacSha512(string value, string key)
        {
            var keyBytes = Convert.FromBase64String(key);
            var valueBytes = Encoding.UTF8.GetBytes(value);
            using (var shaAlgorithm = new HMACSHA512(keyBytes))
            {
                var hashBytes = shaAlgorithm.ComputeHash(valueBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }
        // Rfc 2898 DeriveBytes - PBKDF2
        /// <summary>
        ///     Encrypt Password by Rfc 2898 DeriveBytes - PBKDF2 
        /// </summary>
        public static string HashPassword(string password, string salt, int iterations = 100000)
        {
            byte[] valueBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(valueBytes, saltBytes, iterations, HashAlgorithmName.SHA256))
            {
                var hashBytes = rfc2898DeriveBytes.GetBytes(32);
                var hashString = Convert.ToBase64String(hashBytes);
                return hashString;
            }
        }
        /// <summary>
        ///     Encrypt Password by Rfc 2898 DeriveBytes - PBKDF2 
        /// </summary>
        public static string HashPassword(string password, out string salt, int iterations = 100000)
        {
            salt = GenerateSalt();
            return HashPassword(password, salt, iterations);
        }
        /// <summary>
        ///     Encrypt by Rfc 2898 DeriveBytes - PBKDF2 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key">  </param>
        /// <returns></returns>
        public static string Encrypt(string value, string key)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(value);
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            
            using (var encrypt = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(key, salt, 100000, HashAlgorithmName.SHA256);
                encrypt.Key = pdb.GetBytes(32);
                encrypt.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    ms.Write(salt, 0, salt.Length);
                    using (var cs = new CryptoStream(ms, encrypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    value = Safe64Encoding.EncodeBytes(ms.ToArray());
                }
            }
            return value;
        }
        /// <summary>
        ///     Decrypt by Rfc 2898 DeriveBytes - PBKDF2 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key">  </param>
        /// <returns></returns>
        public static string Decrypt(string value, string key)
        {
            byte[] cipherBytes = Safe64Encoding.DecodeBytes(value);
            if (cipherBytes.Length < 16)
                throw new ArgumentException("Invalid encrypted data - missing salt");
            
            byte[] salt = new byte[16];
            Array.Copy(cipherBytes, 0, salt, 0, 16);
            
            byte[] actualCipherBytes = new byte[cipherBytes.Length - 16];
            Array.Copy(cipherBytes, 16, actualCipherBytes, 0, actualCipherBytes.Length);
            
            using (var encrypt = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(key, salt, 100000, HashAlgorithmName.SHA256);
                encrypt.Key = pdb.GetBytes(32);
                encrypt.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(actualCipherBytes, 0, actualCipherBytes.Length);
                    }
                    value = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return value;
        }
        /// <summary>
        ///     Try to Decrypt by Rfc 2898 DeriveBytes - PBKDF2 
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="key">   </param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryDecrypt(string value, string key, out string result)
        {
            try
            {
                result = Decrypt(value, key);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
            catch (FormatException)
            {
                result = null;
                return false;
            }
            catch (CryptographicException)
            {
                result = null;
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                result = null;
                return false;
            }
        }
        // CRC 32
        public static string HashCrc32(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            Crc32 crc32 = new Crc32();
            var hashBytes = crc32.ComputeHash(bytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            return hash;
        }
    }
}
