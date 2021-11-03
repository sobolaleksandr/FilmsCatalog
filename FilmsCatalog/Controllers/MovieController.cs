namespace FilmsCatalog.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using FilmsCatalog.Data;
    using FilmsCatalog.Models;
    using FilmsCatalog.Services;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контроллер <see cref="Movie"/>
    /// </summary>
    public class MovieController : Controller
    {
        /// <summary>
        /// Контекст данных.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Сервис сохранения изображений.
        /// </summary>
        private readonly IImageSaver _imageSaver;

        /// <summary>
        /// Менджер авторизации.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Контроллер <see cref="Movie"/>
        /// </summary>
        /// <param name="signInManager"> Менджер авторизации. </param>
        /// <param name="context"> Контекст данных. </param>
        /// <param name="imageSaver"> Сервис сохранения изображений. </param>
        public MovieController(SignInManager<User> signInManager, ApplicationDbContext context, IImageSaver imageSaver)
        {
            _context = context;
            _signInManager = signInManager;
            _imageSaver = imageSaver;
        }

        /// <summary>
        /// Отказано в доступе.
        /// </summary>
        /// <returns> Форма отказа в доступе. </returns>
        public IActionResult BadLogin()
        {
            return View();
        }

        /// <summary>
        /// Получить форму редактирования фильмов.
        /// </summary>
        /// <param name="id"> Id-фильма. </param>
        /// <returns> В случае успеха форма редактирования фильмов, иначе <see cref="BadLogin"/> </returns>
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction(nameof(BadLogin));

            if (!id.HasValue)
                return View();

            var movie = await _context.Movies.FirstOrDefaultAsync(p => p.Id == id.Value);
            if (movie == null)
                return View();

            if (movie.Added == User.Identity?.Name)
                return View(movie);

            return RedirectToAction(nameof(BadLogin));
        }

        /// <summary>
        /// Редактировать фильм.
        /// </summary>
        /// <param name="model"> Вью-модель фильма. </param>
        /// <returns> В случае успеха <see cref="Index"/>, в случае невалидности модели обновляет страницу, в случае отсутсвия прав <see cref="BadLogin"/> </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(MovieViewModel model)
        {
            var movie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Year = model.Year,
                Producer = model.Producer,
                Added = User.Identity?.Name,
                Poster = await _imageSaver.SaveFile(model.Poster)
            };

            if (!ModelState.IsValid)
                return View(movie);

            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction(nameof(BadLogin));

            await AddOrUpdateEntity(movie);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Получить форму просмотра информации о фильме.
        /// </summary>
        /// <param name="id"> Id-фильма. </param>
        /// <returns> Форма просмотра информации о фильме. </returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var movie = await _context.Movies.FirstOrDefaultAsync(p => p.Id == id.Value);
            if (movie != null)
                return View(movie);

            return NotFound();
        }

        /// <summary>
        /// Сообщение об ошибках. 
        /// </summary>
        /// <returns> Сообщение об ошибках. </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Форма просмотра всех фильмов. 
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <returns> Форма просмотра всех фильмов с пагинацией. </returns>
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10;
            IQueryable<Movie> source = _context.Movies;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, pageSize);
            var viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Movies = items
            };

            return View(viewModel);
        }

        /// <summary>
        /// Редактировать фильм.
        /// </summary>
        /// <param name="movie"> Модель фильма. </param>
        private async Task AddOrUpdateEntity(Movie movie)
        {
            if (movie.Id == 0)
                await _context.Movies.AddAsync(movie);
            else
                _context.Movies.Update(movie);

            await _context.SaveChangesAsync();
        }
    }
}