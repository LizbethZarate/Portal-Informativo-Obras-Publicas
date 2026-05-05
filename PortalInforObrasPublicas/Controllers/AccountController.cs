using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;

namespace PortalInforObrasPublicas.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AccountController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario model)
        {
            
            if (ModelState.IsValid)
            {
                var usuario = _usuarioService.ValidarUsuario(model.Email, model.PasswordHash);
                if (usuario != null)
                {
                    //Guardar sesion
                    HttpContext.Session.SetString("Usuario", usuario.Nombre);
                    HttpContext.Session.SetString("Rol", usuario.Rol);
                    if (usuario.Rol == "Administrador")
                    {
                        return RedirectToAction("Index", "Obra");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario model, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (model.PasswordHash != ConfirmPassword)
                {
                    ModelState.AddModelError("", "Las contraseñas no coinciden.");
                    return View(model);
                }
                var existe = _usuarioService.ObtenerPorEmail(model.Email);
                if (existe != null)
                {
                    ModelState.AddModelError("", "El correo ya está registrado.");
                    return View(model);
                }
                _usuarioService.RegistrarUsuario(model);
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}