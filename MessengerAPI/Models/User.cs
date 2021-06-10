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
        private static int _count = 0;
        private string _userName;
        private string _email;
        private int _id;
        #endregion

        /// <summary>
        /// Конструткор.
        /// </summary>
        /// <param name="username"> Имя пользователя. </param>
        /// <param name="email"> Почта пользователя. </param>
        private User(string username, string email)
        {
            UserName = username;
            Email = email;
            // TODO: переделать систему ID.
            _id = _count++;
        }

        #region Свойства.

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DataMember]
        public string UserName
        {
            get => _userName;
            private set
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

        /// <summary>
        /// Id пользователя.
        /// </summary>
        [DataMember]
        public int Id => _id;

        #endregion

        #region Методы.

        /// <summary>
        /// Метод для создания пользователей.
        /// </summary>
        /// <param name="username"> Имя пользователя. </param>
        /// <param name="email"> Почта пользователя. </param>
        /// <returns></returns>
        public static User Parse(string username, string email)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is null or empty.");
            }
            if (!EmailChecker.IsValidEmail(email))
            {
                throw new ArgumentException("Incorrect email.");
            }

            return new User(username, email);
        }

        #endregion
    }
}
