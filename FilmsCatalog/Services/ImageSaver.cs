namespace FilmsCatalog.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Сервис сохранения изображений.
    /// </summary>
    public class ImageSaver : IImageSaver
    {
        /// <summary>
        /// Окружение приложения.
        /// </summary>
        private readonly IWebHostEnvironment _appEnvironment;

        /// <summary>
        /// Сервис сохранения изображений.
        /// </summary>
        /// <param name="appEnvironment"> Окружение приложения. </param>
        public ImageSaver(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var path = "/Files/" + file.FileName;
            await using var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
    }
}