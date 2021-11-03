namespace FilmsCatalog.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Модель представления страницы фильмов.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Коллекция фильмов.
        /// </summary>
        public IEnumerable<Movie> Movies { get; set; }

        /// <summary>
        /// Модель страницы.
        /// </summary>
        public PageViewModel PageViewModel { get; set; }
    }
}