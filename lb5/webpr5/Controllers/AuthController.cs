using webpr5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webpr5.Controllers
{
    public class AuthController : Controller
    {
        private AuthService _authService;

        public AuthController(AuthService authService) => _authService = authService;

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            var isAuthenticated = _authService.Login(model);

            if (isAuthenticated)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "not loggined");

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            var result = _authService.Register(model);

            if (result)
                return RedirectToAction("Index", "Home");

            return View(model);
        }
    }
}
