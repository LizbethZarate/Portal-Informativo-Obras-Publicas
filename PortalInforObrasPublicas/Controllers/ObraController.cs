using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;

namespace PortalInforObrasPublicas.Controllers
{
    public class ObraController : Controller
    {
        private readonly ObraService _service;

        public ObraController(ObraService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            var obras = _service.ObtenerTodas();
            return View(obras);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create(Obra obra)
        {
            var mensaje = _service.CrearObra(obra);

            if (!string.IsNullOrEmpty(mensaje))
            {
                ModelState.AddModelError("", mensaje);
                return View(obra);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Obra/Edit/5
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            var obra = _service.ObtenerPorId(id);
            if (obra == null)
                return NotFound();

            return View(obra);
        }

        // POST: Obra/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(Obra obra)
        {
            if (!ModelState.IsValid)
                return View(obra);

            var mensaje = _service.Actualizar(obra);
            if (!string.IsNullOrEmpty(mensaje))
            {
                ModelState.AddModelError("", mensaje);
                return View(obra);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Obra/Delete/5
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            var obra = _service.ObtenerPorId(id);
            if (obra == null)
                return NotFound();

            return View(obra);
        }

        // POST: Obra/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}