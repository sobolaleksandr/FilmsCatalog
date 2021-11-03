namespace FilmsCatalog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель фильма.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Создатель записи. 
        /// </summary>
        public string Added { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Путь до постера.
        /// </summary>
        public string Poster { get; set; }

        /// <summary>
        /// Режисер.
        /// </summary>
        [Required]
        public string Producer { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Год выпуска.
        /// </summary>
        [Required]
        public DateTime? Year { get; set; }
    }
}