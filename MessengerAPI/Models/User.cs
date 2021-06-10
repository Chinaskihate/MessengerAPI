using System;
using System.Runtime.Serialization;

namespace MessengerAPI.Models
{
    /// <summary>
    /// Описывает пользователя.
    /// </summary>
    [DataContract]
    public class User
    {
        #region Поля класса.
        private string _userName;
        private string _email;
        #endregion

        /// <summary>
        /// Конструткор.
        /// </summary>
        /// <param name="userName"> Имя пользователя. </param>
        /// <param name="email"> Почта пользователя. </param>
        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }

        #region Свойства

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DataMember]
        public string UserName
        {
            get => _userName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty or null user name.");
                }

                _userName = value;
            }
        }

        /// <summary>
        /// Почта пользователя.
        /// </summary>
        [DataMember]
        public string Email
        {
            get => _email;
            private set
            {
                if (!EmailChecker.IsValidEmail(value))
                {
                    throw new ArgumentException("Incorrect email.");
                }

                _email = value;
            }
        }

        #endregion
    }
}
