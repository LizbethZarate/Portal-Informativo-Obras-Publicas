using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalInforObrasPublicas.Controllers
{
    public class ObraController : Controller
    {
        // GET: ObraController
        public ActionResult Index()
        {
            return View();
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

        // GET: ObraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ObraController/Edit/5
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

        // GET: ObraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ObraController/Delete/5
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
    }
}
