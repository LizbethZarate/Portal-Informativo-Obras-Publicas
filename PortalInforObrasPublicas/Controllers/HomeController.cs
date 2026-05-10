using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace PortalInforObrasPublicas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ObraService _obraService;
        private readonly ReporteService _reporteService;
        private readonly UsuarioService _usuarioService;


        public HomeController(ILogger<HomeController> logger, 
            ObraService obraService,
            ReporteService reporteService,
            UsuarioService usuarioService)
        {
            _logger = logger;
            _obraService = obraService;
            _reporteService = reporteService;
            _usuarioService = usuarioService;
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
        [Authorize]
        public IActionResult HistorialDenuncias()
        {
            var email = HttpContext.Session.GetString("Usuario");

            if (email == null)
                return RedirectToAction("Login", "Account");

            var idUsuario = _usuarioService.ObtenerIdPorEmail(email);

            if (idUsuario == null)
                return RedirectToAction("Login", "Account");

            var reportes = _reporteService.ObtenerPorUsuario(idUsuario.Value);

            return View(reportes);
        }

        public IActionResult Detalle(int id)
        {
            var obra = _obraService.ObtenerPorId(id);

            if (obra == null)
                return NotFound();

            return View(obra);
        }

        [Authorize]
        [HttpGet]
        public IActionResult NuevaDenuncia(int? idObra)
        {
            ViewBag.Obras = _obraService.ObtenerTodas();
            ViewBag.IdObra = idObra;

            return View(new Reporte
            {
                IdObra = idObra ?? 0
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NuevaDenuncia(Reporte reporte, List<IFormFile> imagenes)
        {
            var email = HttpContext.Session.GetString("Usuario");
            var idUsuario = email != null ? _usuarioService.ObtenerIdPorEmail(email) : null;

            reporte.IdUsuario = idUsuario;

            if (imagenes == null || !imagenes.Any())
            {
                ModelState.AddModelError("", "Debes subir al menos una imagen como evidencia.");
                ViewBag.Obras = _obraService.ObtenerTodas();
                ViewBag.IdObra = reporte.IdObra;
                return View(reporte);
            }

            reporte.Imagenes = new List<ReporteImagen>();

            foreach (var imagen in imagenes)
            {
                if (imagen != null && imagen.Length > 0)
                {
                    var extension = Path.GetExtension(imagen.FileName).ToLower();

                    var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png" };

                    if (!extensionesPermitidas.Contains(extension))
                    {
                        ModelState.AddModelError("", "Solo se permiten imágenes JPG o PNG.");
                        ViewBag.Obras = _obraService.ObtenerTodas();
                        ViewBag.IdObra = reporte.IdObra;
                        return View(reporte);
                    }

                    if (imagen.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "Cada imagen debe pesar máximo 5MB.");
                        ViewBag.Obras = _obraService.ObtenerTodas();
                        ViewBag.IdObra = reporte.IdObra;
                        return View(reporte);
                    }

                    var carpeta = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/uploads/reportes");

                    if (!Directory.Exists(carpeta))
                        Directory.CreateDirectory(carpeta);

                    var nombreArchivo = Guid.NewGuid() + extension;
                    var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        imagen.CopyTo(stream);
                    }

                    reporte.Imagenes.Add(new ReporteImagen
                    {
                        RutaImagen = "/uploads/reportes/" + nombreArchivo
                    });
                }
            }

            var mensaje = _reporteService.CrearReporte(reporte);

            if (!string.IsNullOrEmpty(mensaje))
            {
                ModelState.AddModelError("", mensaje);
                ViewBag.Obras = _obraService.ObtenerTodas();
                ViewBag.IdObra = reporte.IdObra;
                return View(reporte);
            }

            TempData["Mensaje"] = "Denuncia registrada correctamente.";
            return RedirectToAction("HistorialDenuncias");
        }
    }
}
