using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI.Models
{
    internal static class EmailChecker
    {
        /// <summary>
        /// Проверка почты на корректность.
        /// </summary>
        /// <param name="email"> Почта. </param>
        /// <returns> Корректность почты. </returns>
        internal static bool IsValidEmail(string email)
        {
            // TODO: оптимизировать.
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
