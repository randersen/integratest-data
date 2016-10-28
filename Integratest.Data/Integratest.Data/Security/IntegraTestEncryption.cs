using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Integratest.Data.Security
{
    public class IntegraTestEncryption
    {
        public static int PBKDF2_Iterations => 1000;
        public static int PBKDF2_SaltSize => 32;
        public static int PBKDF2_HashSize => 32;


        public static string ComputePasswordHash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            int iterations = PBKDF2_Iterations;
            byte[] salt;
            var hash = ComputePBKDF2(password, PBKDF2_SaltSize, iterations, PBKDF2_HashSize, out salt);
            return FormatPasswordHash(salt, iterations, hash);
        }

        public static byte[] ComputePBKDF2(string password, byte[] salt, int iterations, int hashSize)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(hashSize);
            }
        }

        public static byte[] ComputePBKDF2(string password, int saltSize, int iterations, int hashSize, out byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltSize, iterations))
            {
                salt = pbkdf2.Salt;
                return pbkdf2.GetBytes(hashSize);
            }
        }

        private static string FormatPasswordHash(byte[] salt, int iterations, byte[] hash)
        {
            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }
            if (hash == null)
            {
                throw new ArgumentNullException("hash");
            }

            //var byteIterations =
                
            var bytes = BitConverter.GetBytes(iterations);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }


            return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(bytes) + "." + Convert.ToBase64String(hash);
        }

        private static bool TryParsePasswordHash(string passwordHash, out byte[] salt, out int iterations, out byte[] hash)
        {
            if (passwordHash == null)
            {
                throw new ArgumentNullException("passwordHash");
            }

            try
            {
                var parts = passwordHash.Split('.');
                salt = parts[0].FromBase64ToBytes();
                iterations = parts[1].FromBase64ToBytes().ToInt32(0);
                hash = parts[2].FromBase64ToBytes();
                return true;
            }
            catch
            {
                salt = default(byte[]);
                iterations = default(int);
                hash = default(byte[]);
                return false;
            }
        }

        public static bool IsCorrectPassword(string password, string passwordHash)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (passwordHash == null)
            {
                throw new ArgumentNullException("passwordHash");
            }

            byte[] salt;
            int iterations;
            byte[] correctHash;
            if (TryParsePasswordHash(passwordHash, out salt, out iterations, out correctHash))
            {
                var testHash = ComputePBKDF2(password, salt, iterations, correctHash.Length);
                return correctHash.CryptoEquals(testHash);
            }
            else
            {
                throw new Exception("Invalid password hash.");
            }
        }




    }
}
