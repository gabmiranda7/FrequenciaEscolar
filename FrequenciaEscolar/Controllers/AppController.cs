using Microsoft.AspNetCore.Mvc;

namespace FrequenciaEscolar.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app", "index.html"), "text/html");
        }
    }
}
