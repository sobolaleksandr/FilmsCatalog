namespace FilmsCatalog.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель авторизации.
    /// </summary>
    public class SignInViewModel
    {
        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Запоминание пароля.
        /// </summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Строка перенаправления.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}