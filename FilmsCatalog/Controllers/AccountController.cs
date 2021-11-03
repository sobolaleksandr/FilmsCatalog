namespace FilmsCatalog.Controllers
{
    using System.Threading.Tasks;

    using FilmsCatalog.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Контроллер авторизации.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Логгер.
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Менеджер авторизации.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Менеджер пользователей.
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Контроллер авторизации.
        /// </summary>
        /// <param name="userManager"> Менеджер пользователей. </param>
        /// <param name="signInManager"> Менеджер авторизации. </param>
        /// <param name="logger"> Логгер. </param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Выход.
        /// </summary>
        /// <returns> <see cref="MovieController.Index"/></returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }

        /// <summary>
        /// Получить форму авторизации.
        /// </summary>
        /// <param name="returnUrl"> Строка перенаправления. </param>
        /// <returns> Форма авторизации. </returns>
        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            return View(new SignInViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="model"> Модель</param>
        /// <returns> <see cref="MovieController.Index"/> если пользователь авторизован, иначе форму авторизации.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var signIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!signIn.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Movie");
        }

        /// <summary>
        /// Получить форму регистрации. 
        /// </summary>
        /// <returns> Форма регистрации. </returns>
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// Зарегестрироваться.
        /// </summary>
        /// <param name="model"> Модель регистрации. </param>
        /// <returns> <see cref="MovieController.Index"/> в случае успеха, иначе обновление страницы. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName
            };

            var created = await _userManager.CreateAsync(user, model.Password);
            if (created.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation("User success signup!");

                return RedirectToAction("Index", "Movie");
            }

            foreach (var error in created.Errors)
            {
                _logger.LogError("User don't signup!");
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}