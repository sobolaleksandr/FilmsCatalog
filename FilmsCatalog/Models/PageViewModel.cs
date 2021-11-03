namespace FilmsCatalog.Models
{
    using System;

    /// <summary>
    /// Вью модель страницы.
    /// </summary>
    public class PageViewModel
    {
        /// <summary>
        /// Вью модель страницы.
        /// </summary>
        /// <param name="count"> Общее число объектов. </param>
        /// <param name="pageNumber"> Номер текущей страницы. </param>
        /// <param name="pageSize"> Количество объектов на странице. </param>
        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        /// <summary>
        /// Есть следующая страница.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// Есть предыдущая страница.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Общее число страниц.
        /// </summary>
        public int TotalPages { get; }
    }
}