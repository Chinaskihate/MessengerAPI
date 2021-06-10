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
        private int _senderId;
        private int _receiverId;
        #endregion

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="subject"> Тема сообщения. </param>
        /// <param name="content"> Содержимое сообщения. </param>
        /// <param name="senderEmail"> Почта отправителя. </param>
        /// <param name="receiverEmail"> Почта получателя. </param>
        public Message(string subject, string content, int senderId, int receiverId)
        {
            Subject = subject;
            Content = content;
            _senderId = senderId;
            _receiverId = receiverId;
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
        public int SenderId => _senderId;

        /// <summary>
        /// Получатель.
        /// </summary>
        [DataMember]
        //TODO: добавить валидацию
        public int ReceiverId => _receiverId;

        #endregion
    }
}
