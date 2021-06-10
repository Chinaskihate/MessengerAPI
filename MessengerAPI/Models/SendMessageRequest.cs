using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI.Models
{
    /// <summary>
    /// Запрос на отправление сообщения.
    /// </summary>
    public class SendMessageRequest
    {
        #region Свойства класса.

        /// <summary>
        /// ID отправителя.
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// ID получателя.
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Содержимое сообщения.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Тема сообщения.
        /// </summary>
        public string Subject { get; set; }

        #endregion
    }
}
