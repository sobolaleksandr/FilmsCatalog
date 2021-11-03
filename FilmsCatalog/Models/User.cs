namespace FilmsCatalog.Models
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; }
    }
}