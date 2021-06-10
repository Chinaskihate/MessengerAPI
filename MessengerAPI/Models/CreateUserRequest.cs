namespace MessengerAPI.Models
{
    /// <summary>
    /// Запрос на создание пользователя.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; set; }
    }
}
