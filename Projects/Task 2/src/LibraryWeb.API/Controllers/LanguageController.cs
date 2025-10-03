using LibraryWeb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : Controller
    {
        private readonly LanguageService languageService;

        public LanguageController(LanguageService languageService)
        {
            this.languageService = languageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public 
    }
}
