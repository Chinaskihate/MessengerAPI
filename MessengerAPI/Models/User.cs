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
        private static long _count = 1;
        private string _userName;
        private string _email;
        private long _id;
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
            // TODO: переделать систему ID.
            _id = _count++;
        }

        #region Свойства

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
        public long Id => _id;

        #endregion
    }
}
