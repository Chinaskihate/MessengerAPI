using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Models;
using System.IO;
using System;
using System.Runtime.Serialization.Json;

namespace MessengerAPI.Controllers
{
    /// <summary>
    /// Контроллер для генерации пользователей и сообщений.
    /// </summary>
    public class RandomGeneratorController : Controller
    {
        #region Поля класса.
        private static List<string> text;
        private static Random rnd = new Random();
        #endregion

        /// <summary>
        /// Статический конструктор.
        /// </summary>
        static RandomGeneratorController()
        {
            text = System.IO.File.ReadAllLines("text.txt").ToList();
        }

        /// <summary>
        /// Генерация пользователей и сообщений.
        /// </summary>
        /// <returns></returns>
        [HttpGet("create-random")]
        public IActionResult CreateRandomUsersAndMessages([FromQuery] int usersCount, int messagesCount)
        {
            if (usersCount > 100 || usersCount < 0)
            {
                return BadRequest("The number of users must be in the range from 0 to 100.");
            }
            if (messagesCount > 100 || messagesCount < 0)
            {
                return BadRequest("The number of messages must be in the range from 0 to 100.");
            }

            UserController.Users = CreateRandomUsers(usersCount);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<User>));
            try
            {
                using (FileStream fs = new FileStream("users.json", FileMode.OpenOrCreate))
                    ser.WriteObject(fs, UserController.Users);
            }
            catch (Exception ex)
            {
                string foo = ex.Message;
            };

            MessageController.Messages = CreateRandomMessages(UserController.Users, messagesCount);
            ser = new DataContractJsonSerializer(typeof(List<Message>));
            try
            {
                using (FileStream fs = new FileStream("messages.json", FileMode.OpenOrCreate))
                    ser.WriteObject(fs, MessageController.Messages);
            }
            catch { };
            return Ok("Random users and messages are created.");
        }

        /// <summary>
        /// Создание случайных пользователей.
        /// </summary>
        /// <param name="usersCount"> Количество пользователей. </param>
        /// <param name="wordsCount"> Количество слов в почте и нике. </param>
        /// <returns></returns>
        private List<User> CreateRandomUsers(int usersCount, int wordsCount = 3)
        {
            List<User> users = new List<User>();
            string email = string.Empty;
            string username = string.Empty;
            int index = 0;
            for (int i = 0; i < usersCount; i++)
            {
                email = string.Empty;
                username = string.Empty;
                for (int j = 0; j < wordsCount; j++)
                {
                    index = rnd.Next(0, text.Count);
                    email += text[index];
                }
                email += "@gmail.com";
                for (int j = 0; j < wordsCount; j++)
                {
                    index = rnd.Next(0, text.Count);
                    username += text[index];
                }
                users.Add(Models.User.Parse(username, email));
            }
            return users;
        }

        /// <summary>
        /// Создание случайных сообщений.
        /// </summary>
        /// <param name="users"> Список пользователей. </param>
        /// <param name="messagesCount"> Количество сообщений. </param>
        /// <param name="wordsCount"> Количество слов в содержимом сообщения. </param>
        /// <returns></returns>
        private List<Message> CreateRandomMessages(List<User> users, int messagesCount, int wordsCount = 10)
        {
            List<Message> messages = new List<Message>();
            string content;
            string subject;
            int senderId;
            int receiverId;
            int index;
            for (int i = 0; i < messagesCount; i++)
            {
                content = string.Empty;
                subject = string.Empty;
                for (int j = 0; j < wordsCount; j++)
                {
                    index = rnd.Next(0, text.Count);
                    content += $"{text[index]} ";
                }
                for (int j = 0; j < 3; j++)
                {
                    index = rnd.Next(0, text.Count);
                    subject += $"{text[index]} ";
                }
                senderId = rnd.Next(0, users.Count);
                receiverId = rnd.Next(0, users.Count);
                messages.Add(Message.Parse(subject, content, senderId, receiverId));
            }
            return messages;
        }
    }
}
