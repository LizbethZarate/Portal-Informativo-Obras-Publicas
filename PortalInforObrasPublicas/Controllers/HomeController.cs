using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;
using System.Diagnostics;

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

        public IActionResult Index(string buscar, string estado)
        {
            var obras = _obraService.ObtenerTodas();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                obras = obras.Where(o =>
                    o.Nombre.Contains(buscar, StringComparison.OrdinalIgnoreCase) ||
                    (o.Ubicacion != null && o.Ubicacion.Contains(buscar, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(estado) && estado != "Todos")
            {
                obras = obras.Where(o => o.Estado == estado).ToList();
            }

            ViewBag.Buscar = buscar;
            ViewBag.Estado = estado;

            return View(obras);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId =
                    Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
        public IActionResult HistorialDenuncias()
        {
            return View();
        }

        public IActionResult Detalle(int id)
        {
            var obra = _obraService.ObtenerPorId(id);

            if (obra == null)
                return NotFound();

            return View(obra);
        }
    }
}
