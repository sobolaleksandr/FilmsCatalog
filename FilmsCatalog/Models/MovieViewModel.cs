namespace FilmsCatalog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Вью модель фильма.
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
        private readonly List<string> _resolutions = new List<string>()
        {
            "image/jpeg",
            "image/bmp",
            "image/jpg",
            "image/gif",
            "image/png",
            "image/png"
        };

        public string Description { get; set; }

        public int ID { get; set; }

        public IFormFile Poster { get; set; }

        public string Producer { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Year { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Title))
                errors.Add(new ValidationResult("Введите название фильма!", new List<string> { nameof(Title) }));
            if (string.IsNullOrWhiteSpace(Description))
                errors.Add(new ValidationResult("Введите описание фильма!", new List<string> { nameof(Description) }));
            if (!Year.HasValue)
                errors.Add(new ValidationResult("Введите год выпуска!", new List<string> { nameof(Year) }));
            else if (Year.Value > DateTime.Today || Year.Value == default)
                errors.Add(new ValidationResult("Неверный год выпуска фильма!", new List<string> { nameof(Year) }));
            if (!ValidateImage(Poster))
                errors.Add(new ValidationResult("Неверный тип файла!", new List<string> { nameof(Poster) }));
            if (string.IsNullOrWhiteSpace(Producer))
                errors.Add(new ValidationResult("Введите режисера фильма!", new List<string> { nameof(Producer) }));

            return errors;
        }

        /// <summary>
        /// Валидация загружаемого файла. 
        /// </summary>
        /// <param name="uploadedFile"> Загружаемый файл. </param>
        /// <returns> Возвращает true, если пройдена. </returns>
        private bool ValidateImage(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                return false;

            return _resolutions.Contains(uploadedFile.ContentType) && uploadedFile.Length < MAX_SIZE_IN_BYTES;
        }
    }
}