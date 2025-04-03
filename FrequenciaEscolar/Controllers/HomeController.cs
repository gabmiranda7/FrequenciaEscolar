using System.Diagnostics;
using FrequenciaEscolar.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrequenciaEscolar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
