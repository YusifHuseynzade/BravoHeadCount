using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=-";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            // Şifreyi belirtilen uzunlukta oluştur
            for (int i = 0; i < length; i++)
            {
                // Rastgele bir karakter seç
                int index = random.Next(validChars.Length);
                sb.Append(validChars[index]);
            }

            return sb.ToString();
        }
    }
}
