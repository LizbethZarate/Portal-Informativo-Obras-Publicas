using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;

namespace PortalInforObrasPublicas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ObraService _obraService;

        public HomeController(ILogger<HomeController> logger, ObraService obraService)
        {
            _logger = logger;
            _obraService = obraService;
        }   


        public IActionResult Index()
        {
            var obras = _obraService.ObtenerTodas();
            return View(obras);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
