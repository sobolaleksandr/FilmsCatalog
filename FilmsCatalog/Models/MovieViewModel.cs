namespace FilmsCatalog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Вью модель <see cref="Movie"/>.
    /// </summary>
    public class MovieViewModel : IValidatableObject
    {
        /// <summary>
        /// Максимальный размер постера в байтах.
        /// </summary>
        private const long MAX_SIZE_IN_BYTES = 1500000;

        /// <summary>
        /// Допустимые расширения постера.
        /// </summary>
        private readonly List<string> _resolutions = new List<string>
        {
            "image/jpeg",
            "image/bmp",
            "image/jpg",
            "image/gif",
            "image/png",
            "image/png"
        };

        /// <summary>
        /// Список ошибок модели.
        /// </summary>
        private List<ValidationResult> _errors;

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Файл постера.
        /// </summary>
        public IFormFile Poster { get; set; }

        /// <summary>
        /// Режисер.
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Год выпуска.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? Year { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            _errors = new List<ValidationResult>();
            ValidateStringProperty(Title, "Введите название фильма!");
            ValidateStringProperty(Description, "Введите описание фильма!");
            ValidateStringProperty(Producer, "Введите режисера фильма!");
            ValidateFileProperty(Poster, "Неверный тип файла!");

            if (!Year.HasValue)
                _errors.Add(new ValidationResult("Введите год выпуска!", new[] { nameof(Year) }));
            else if (Year.Value > DateTime.Today || Year.Value == default)
                _errors.Add(new ValidationResult("Неверный год выпуска фильма!", new[] { nameof(Year) }));

            return _errors;
        }

        /// <summary>
        /// Валидация поля типа файл. 
        /// </summary>
        /// <param name="property"> Файл. </param>
        /// <param name="errorDescription"> Текст ошибки. </param>
        private void ValidateFileProperty(IFormFile property, string errorDescription)
        {
            if (ValidateImage(property))
                _errors.Add(new ValidationResult(errorDescription, new[] { nameof(property) }));
        }

        /// <summary>
        /// Валидация загружаемого файла. 
        /// </summary>
        /// <param name="uploadedFile"> Загружаемый файл. </param>
        /// <returns> Возвращает true, если пройдена. </returns>
        private bool ValidateImage(IFormFile uploadedFile)
        {
            return uploadedFile == null || !_resolutions.Contains(uploadedFile.ContentType) ||
                   uploadedFile.Length >= MAX_SIZE_IN_BYTES;
        }

        /// <summary>
        /// Валидация строковго поля.
        /// </summary>
        /// <param name="property"> Валидируемое поле. </param>
        /// <param name="errorDescription"> Текст ошибки. </param>
        private void ValidateStringProperty(string property, string errorDescription)
        {
            if (string.IsNullOrWhiteSpace(property))
                _errors.Add(new ValidationResult(errorDescription, new[] { nameof(property) }));
        }
    }
}