namespace FilmsCatalog.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Сервис сохранения изображений.
    /// </summary>
    public interface IImageSaver
    {
        /// <summary>
        /// Сохранить изображение.
        /// </summary>
        /// <param name="file"> Файл изображения. </param>
        /// <returns> Путь до изображения. </returns>
        Task<string> SaveFile(IFormFile file);
    }
}