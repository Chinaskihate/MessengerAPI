using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Models;

namespace MessengerAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем.
    /// </summary>
    public class UserController : Controller
    {
        #region Поля класса.
        private static List<User> _users = new List<User>();
        #endregion

        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="req"> Запрос. </param>
        /// <returns></returns>
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] CreateUserRequest req)
        {
            //TODO: добавить валидацию.
            var user = new User(req.UserName, req.Email);
            if (_users.Any(x => x.Email != req.Email))
            {
                _users.Add(user);
            }
            return Ok(user);
        }

        /// <summary>
        /// Получение информации о пользователе по почте.
        /// </summary>
        /// <param name="email"> Почта. </param>
        /// <returns></returns>
        [HttpGet("get-user-by-email")]
        public IActionResult GetUserById([FromQuery] string email)
        {
            var result = _users.Where(x => x.Email == email).ToList();
            if (result.Count == 0)
            {
                return NotFound(new { Message = $"Пользователь с почтой {email} не найден" });
            }
            return Ok(result.First());
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers() => Ok(_users);
    }
}
