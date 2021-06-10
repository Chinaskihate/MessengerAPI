using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Models;

namespace MessengerAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с сообщениями.
    /// </summary>
    public class MessageController : Controller
    {
        private static List<Message> _messages = new List<Message>();

        [HttpPost("/send-message")]
        public IActionResult SendMessage([FromBody] SendMessageRequest req)
        {
            //TODO: добавить валидацию.
            Message message = new Message(req.Subject, req.Content, req.SenderId, req.ReceiverId);
            _messages.Add(message);
            return Ok(message);
        }

        [HttpGet("/get-messages")]
        public IActionResult GetMessagesBySenderAndReceiver([FromQuery] int senderId, [FromQuery] int receiverId)
        {
            var result = _messages.Where(x => x.SenderId == senderId 
                                           && x.ReceiverId== receiverId);
            return Ok(result);
        }
    }
}
