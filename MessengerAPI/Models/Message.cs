using System;
using System.Runtime.Serialization;

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
        private int _senderId;
        private int _receiverId;
        #endregion

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="subject"> Тема сообщения. </param>
        /// <param name="content"> Содержимое сообщения. </param>
        /// <param name="senderId"> Почта отправителя. </param>
        /// <param name="receiverId"> Почта получателя. </param>
        private Message(string subject, string content, int senderId, int receiverId)
        {
            Subject = subject;
            Content = content;
            SenderId = senderId;
            ReceiverId = receiverId;
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
        //TODO: добавить валидацию
        public int SenderId
        {
            get => _senderId;
            private set
            {
                _senderId = value;
            }
        }

        /// <summary>
        /// Получатель.
        /// </summary>
        [DataMember]
        //TODO: добавить валидацию
        public int ReceiverId
        {
            get => _receiverId;
            private set
            {
                _receiverId = value;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод для создания сообщений.
        /// </summary>
        /// <param name="subject"> Тема сообщения. </param>
        /// <param name="content"> Содержимое сообщения. </param>
        /// <param name="senderId"> ID отправителя. </param>
        /// <param name="receiverId"> ID получателя. </param>
        /// <returns></returns>
        public static Message Parse(string subject, string content, int senderId, int receiverId)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("Empty message subject.");
            }
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Empty message content.");
            }

            return new Message(subject, content, senderId, receiverId);
        }

        #endregion
    }
}
