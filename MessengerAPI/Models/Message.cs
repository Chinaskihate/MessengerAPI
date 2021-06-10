using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MessengerAPI.Models
{
    /// <summary>
    /// Описывает сообщение между пользователями.
    /// </summary>
    [DataContract]
    public class Message
    {
        #region Поля класса.
        private string _subject;
        private string _content;
        private string _senderEmail;
        private string _receiverEmail;
        #endregion

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="subject"> Тема сообщения. </param>
        /// <param name="content"> Содержимое сообщения. </param>
        /// <param name="senderEmail"> Почта отправителя. </param>
        /// <param name="receiverEmail"> Почта получателя. </param>
        public Message(string subject, string content, string senderEmail, string receiverEmail)
        {
            Subject = subject;
            Content = content;
            SenderEmail = senderEmail;
            ReceiverEmail = receiverEmail;
        }

        #region Свойства.

        /// <summary>
        /// Тема сообщения.
        /// </summary>
        [DataMember]
        public string Subject
        {
            get => _subject;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty message subject.");
                }

                _subject = value;
            }
        }

        /// <summary>
        /// Содержимое сообщения.
        /// </summary>
        [DataMember]
        public string Content
        {
            get => _content;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty message content.");
                }

                _content = value;
            }
        }

        /// <summary>
        /// Отправитель.
        /// </summary>
        [DataMember]
        public string SenderEmail
        {
            get => _senderEmail;
            set
            {
                if (!EmailChecker.IsValidEmail(value))
                {
                    throw new ArgumentException("Incorrect sender email.");
                }

                _senderEmail = value;
            }
        }

        /// <summary>
        /// Получатель.
        /// </summary>
        [DataMember]
        public string ReceiverEmail
        {
            get => _receiverEmail;
            set
            {
                if (EmailChecker.IsValidEmail(value))
                {
                    throw new ArgumentException("Incorrect receiver email.");
                }

                _receiverEmail = value;
            }
        }

        #endregion
    }
}
