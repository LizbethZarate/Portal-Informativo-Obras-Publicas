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
        public IActionResult Create(Obra obra, IFormFile imagen)
        {
            if (!ModelState.IsValid)
                return View(obra);

            var mensaje = _service.ValidarObra(obra);

            if (!string.IsNullOrEmpty(mensaje))
            {
                ModelState.AddModelError("", mensaje);
                return View(obra);
            }

            // GUARDAR IMAGEN
            if (imagen != null && imagen.Length > 0)
            {
                var carpeta = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/obras");

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                var nombreArchivo =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(imagen.FileName);

                var rutaCompleta =
                    Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(
                    rutaCompleta,
                    FileMode.Create))
                {
                    imagen.CopyTo(stream);
                }

                obra.Imagenes = new List<ObraImagen>
                {
                    new ObraImagen
                    {
                        RutaImagen = "/uploads/obras/" + nombreArchivo
                    }
                };
            }

            _service.Crear(obra);

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

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(Obra obra, IFormFile imagen)
        {
            if (!ModelState.IsValid)
                return View(obra);

            var obraBD = _service.ObtenerPorId(obra.IdObra);

            if (obraBD == null)
                return NotFound();

            obraBD.Nombre = obra.Nombre;
            obraBD.CodigoSNIP = obra.CodigoSNIP;
            obraBD.Ubicacion = obra.Ubicacion;
            obraBD.Estado = obra.Estado;
            obraBD.Presupuesto = obra.Presupuesto;
            obraBD.FechaInicio = obra.FechaInicio;
            obraBD.FechaFin = obra.FechaFin;

            var mensaje = _service.ValidarObra(obraBD);

            if (!string.IsNullOrEmpty(mensaje))
            {
                ModelState.AddModelError("", mensaje);
                return View(obra);
            }

            if (imagen != null && imagen.Length > 0)
            {
                var carpeta = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/obras");

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                var nombreArchivo =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(imagen.FileName);

                var rutaCompleta =
                    Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(
                    rutaCompleta,
                    FileMode.Create))
                {
                    imagen.CopyTo(stream);
                }

                // SI YA EXISTE IMAGEN
                if (obraBD.Imagenes != null &&
                    obraBD.Imagenes.Any())
                {
                    obraBD.Imagenes.First().RutaImagen =
                        "/uploads/obras/" + nombreArchivo;
                }
                else
                {
                    obraBD.Imagenes = new List<ObraImagen>
                    {
                        new ObraImagen
                        {
                            RutaImagen =
                                "/uploads/obras/" + nombreArchivo
                        }
                    };
                }
            }

            _service.Actualizar(obraBD);

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