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
        /// <summary>
        /// Статический конструктор.
        /// </summary>
        static MessageController()
        {
            // TODO: добавить загрузку из файла.
            Messages = new List<Message>();
        }

        /// <summary>
        /// Список сообщений.
        /// </summary>
        internal static List<Message> Messages { get; set; }

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (Messages.TrueForAll(x => x.ReceiverId != req.ReceiverId))
            {
                return NotFound($"Receiver {req.ReceiverId} doesn't exist.");
            }
            if (Messages.TrueForAll(x => x.SenderId != req.SenderId))
            {
                return NotFound($"Receiver {req.SenderId} doesn't exist.");
            }
            Messages.Add(message);
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
            var result = Messages.Where(x => x.SenderId == senderId
                                           && x.ReceiverId == receiverId);
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
            var result = Messages.Where(x => x.SenderId == senderId);
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
            var result = Messages.Where(x => x.ReceiverId == receiverId);
            return Ok(result);
        }
    }
}
