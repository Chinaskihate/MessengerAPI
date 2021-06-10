using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Models;
using System;

namespace MessengerAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Статический конструктор.
        /// </summary>
        static UserController()
        {
            //TODO: добавить загрузку из файла.
            Users = new List<User>();
        }

        /// <summary>
        /// Список пользователей.
        /// </summary>
        internal static List<User> Users{get;set;}

        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="req"> Запрос. </param>
        /// <returns></returns>
        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] CreateUserRequest req)
        {
            User user = null;
            try
            {
                user = Models.User.Parse(req.UserName, req.Email);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (Users.Any(x => x.Email != req.Email))
            {
                Users.Add(user);
                Users = Users.OrderBy(x => x.Email).ToList();
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
            var result = Users.Where(x => x.Email == email).ToList();
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
        public IActionResult GetAllUsers([FromQuery] int limit, int offset)
        {
            if (offset < 0)
            {
                return BadRequest("Offest can't be negative.");
            }
            if (limit <= 0)
            {
                return BadRequest("Limit must be positive.");
            }
            var result = new List<User>();
            for (int i = offset; i < limit; i++)
            {
                if (i == Users.Count)
                {
                    break;
                }
                result.Add(Users[i]);
            }
            return Ok(result);
        }
    }
}
