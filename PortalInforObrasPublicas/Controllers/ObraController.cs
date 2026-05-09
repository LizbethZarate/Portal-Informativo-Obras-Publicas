using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using PortalInforObrasPublicas.Data;
using PortalInforObrasPublicas.Models;
=======
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6

namespace PortalInforObrasPublicas.Controllers
{
    public class ObraController : Controller
    {
<<<<<<< HEAD
        private readonly AppDbContext _context;

        public ObraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ObraController
        public ActionResult Index()
=======
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
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
        {
            var obras = _context.Obras.ToList();
            return View(obras);
        }

        [HttpPost]
<<<<<<< HEAD
        [ValidateAntiForgeryToken]
        public ActionResult Create(Obra obra)
        {
            if (ModelState.IsValid)
            {
                _context.Obras.Add(obra);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
=======
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
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
        }

        // GET: Obra/Edit/5
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
<<<<<<< HEAD
            var obra = _context.Obras.Find(id);
            if (obra == null) return NotFound();
=======
            var obra = _service.ObtenerPorId(id);
            if (obra == null)
                return NotFound();

>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
            return View(obra);
        }

        // POST: Obra/Edit/5
        [HttpPost]
<<<<<<< HEAD
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Obra obra)
=======
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(Obra obra)
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
        {
            if (!ModelState.IsValid)
                return View(obra);

<<<<<<< HEAD
            var obraDb = _context.Obras.Find(obra.IdObra);
            if (obraDb == null) return NotFound();

            obraDb.Nombre = obra.Nombre;
            obraDb.Ubicacion = obra.Ubicacion;
            obraDb.Estado = obra.Estado;
            obraDb.Presupuesto = obra.Presupuesto;
            obraDb.FechaInicio = obra.FechaInicio;
            obraDb.FechaFin = obra.FechaFin;

            _context.SaveChanges();
=======
            var mensaje = _service.Actualizar(obra);
            if (!string.IsNullOrEmpty(mensaje))
            {
                ModelState.AddModelError("", mensaje);
                return View(obra);
            }
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6

            return RedirectToAction(nameof(Index));
        }

        // GET: Obra/Delete/5
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
<<<<<<< HEAD
            var obra = _context.Obras.Find(id);
            if (obra == null) return NotFound();
            return View(obra);
        }

        // POST: ObraController/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmar(int id)
        {
            var obra = _context.Obras.Find(id);
            if (obra != null)
            {
                _context.Obras.Remove(obra);
                _context.SaveChanges();
            }
=======
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
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
            return RedirectToAction(nameof(Index));
        }
    }
}