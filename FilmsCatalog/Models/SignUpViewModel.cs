namespace FilmsCatalog.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель регистрации пользователя.
    /// </summary>
    public class SignUpViewModel
    {
        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        [EmailAddress(ErrorMessage = "Необходимо указать корректный email")]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя*")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль*")]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердить пароль*")]
        public string PasswordConfirm { get; set; }
    }
}