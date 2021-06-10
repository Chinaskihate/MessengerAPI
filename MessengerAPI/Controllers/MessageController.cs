using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Models;
using System;

namespace MessengerAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с сообщениями.
    /// </summary>
    public class MessageController : Controller
    {
        #region Поля класса.
        private static List<Message> _messages = new List<Message>();
        #endregion

        /// <summary>
        /// Отправление сообщения.
        /// </summary>
        /// <param name="req"> Запрос на отправку. </param>
        /// <returns></returns>
        [HttpPost("/send-message")]
        public IActionResult SendMessage([FromBody] SendMessageRequest req)
        {
            Message message = null;
            try
            {
                message = Message.Parse(req.Subject, req.Content, req.SenderId, req.ReceiverId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (_messages.TrueForAll(x => x.ReceiverId != req.ReceiverId))
            {
                return BadRequest($"Receiver {req.ReceiverId} doesn't exist.");
            }
            if (_messages.TrueForAll(x => x.SenderId != req.SenderId))
            {
                return BadRequest($"Receiver {req.SenderId} doesn't exist.");
            }
            _messages.Add(message);
            return Ok(message);
        }

        /// <summary>
        /// Получение списка сообщений между двумя пользователями.
        /// </summary>
        /// <param name="senderId"> ID отправителя. </param>
        /// <param name="receiverId"> ID получателя. </param>
        /// <returns></returns>
        [HttpGet("/get-messages")]
        public IActionResult GetMessagesBySenderAndReceiver([FromQuery] int senderId, [FromQuery] int receiverId)
        {
            var result = _messages.Where(x => x.SenderId == senderId 
                                           && x.ReceiverId== receiverId);
            return Ok(result);
        }

        /// <summary>
        /// Получение списка сообщений(по отправителю).
        /// </summary>
        /// <param name="senderId"> ID отправителя. </param>
        /// <returns></returns>
        [HttpGet("/get-messages-by-sender")]
        public IActionResult GetMessagesBySender([FromQuery] int senderId)
        {
            var result = _messages.Where(x => x.SenderId == senderId);
            return Ok(result);
        }

        /// <summary>
        /// Получение списка сообщений(по получателю).
        /// </summary>
        /// <param name="receiverId"> ID получателя. </param>
        /// <returns></returns>
        [HttpGet("/get-messages-by-receiver")]
        public IActionResult GetMessagesByReceiver([FromQuery] int receiverId)
        {
            var result = _messages.Where(x => x.ReceiverId == receiverId);
            return Ok(result);
        }
    }
}
