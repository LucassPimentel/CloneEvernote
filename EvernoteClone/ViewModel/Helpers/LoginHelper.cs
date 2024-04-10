using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EvernoteClone.ViewModel.Helpers
{
    public class LoginHelper
    {
        private static int keySize = 64;
        private static int iteractions = 350000;
        private static HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA512;

        private static string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(64);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iteractions,
                hashAlgorithmName,
                keySize);

            var stringHash = Convert.ToHexString(hash);
            return stringHash;

        }

        private static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iteractions, hashAlgorithmName, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private static bool AlreadyExistsAccountByEmail(string name)
        {
            return DatabaseHelper.Read<User>().Exists(u => u.Name == name);
        }

        public static bool Register(User user)
        {
            if (AlreadyExistsAccountByEmail(user.Name))
            {
                MessageBox.Show("Parece que você está tentando criar uma nova conta com um nome que já está em uso. \nUtilize um nome diferente.", "Alerta", MessageBoxButton.OK);
                return false;
            }

            var hashPassword = HashPassword(user.Password, out var salt);

            user.Password = hashPassword;
            user.ConfirmPassword = hashPassword;
            user.Salt = salt;

            var userAdded = DatabaseHelper.Insert(user);
            return userAdded;
        }

        public static bool Login(User user)
        {
            var returnedUser = DatabaseHelper.Read<User>()
                .Where(u => u.Name == user.Name && VerifyPassword(user.Password, u.Password, u.Salt))
                .FirstOrDefault();

            if (returnedUser == null)
            {
                MessageBox.Show("Por favor, verifique se o nome e a senha estão corretos e tente novamente.", "Alerta", MessageBoxButton.OK);
                return false;
            }

            App.UserId = returnedUser.Id.ToString();

            return true;
        }
    }
}
