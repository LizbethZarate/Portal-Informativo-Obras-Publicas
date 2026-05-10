using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Services;

namespace PortalInforObrasPublicas.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReporteController : Controller
    {

        private readonly ReporteService _service;

        public ReporteController(ReporteService service)
        {
            _service = service;
        }

        // GET: ReporteController
        public ActionResult Index()
        {
            var reportes = _service.ObtenerTodos();
            return View(reportes);
        }

        // GET: ReporteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReporteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReporteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReporteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReporteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReporteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReporteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Gestionar(int idReporte, string estado, string prioridad, string? observacionRespuesta)
        {
            var mensaje = _service.ActualizarGestion(idReporte, estado, prioridad, observacionRespuesta);

            if (!string.IsNullOrEmpty(mensaje))
                TempData["Error"] = mensaje;
            else
                TempData["Mensaje"] = "Denuncia actualizada correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
