using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Data;
using PortalInforObrasPublicas.Models;

namespace PortalInforObrasPublicas.Controllers
{
    public class ObraController : Controller
    {
        private readonly AppDbContext _context;

        public ObraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ObraController
        public ActionResult Index()
        {
            var obras = _context.Obras.ToList();
            return View(obras);
        }

        // GET: ObraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObraController/Create
        [HttpPost]
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
        }

        // GET: ObraController/Edit/5
        public ActionResult Edit(int id)
        {
            var obra = _context.Obras.Find(id);
            if (obra == null) return NotFound();
            return View(obra);
        }

        // POST: ObraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Obra obra)
        {
            if (!ModelState.IsValid)
                return View(obra);

            var obraDb = _context.Obras.Find(obra.IdObra);
            if (obraDb == null) return NotFound();

            obraDb.Nombre = obra.Nombre;
            obraDb.Ubicacion = obra.Ubicacion;
            obraDb.Estado = obra.Estado;
            obraDb.Presupuesto = obra.Presupuesto;
            obraDb.FechaInicio = obra.FechaInicio;
            obraDb.FechaFin = obra.FechaFin;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: ObraController/Delete/5
        public ActionResult Delete(int id)
        {
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
            return RedirectToAction(nameof(Index));
        }
    }
}
