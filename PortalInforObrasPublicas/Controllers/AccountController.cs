using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PortalInforObrasPublicas.Models;
using PortalInforObrasPublicas.Services;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioService.ValidarUsuario(
                    model.Email,
                    model.Password);

                if (usuario != null)
                {
                    HttpContext.Session.SetString("Usuario", usuario.Email);
                    HttpContext.Session.SetString("Nombre", usuario.Nombre);
                    HttpContext.Session.SetString("Rol", usuario.Rol);

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

                    var identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    // LOGIN CON COOKIE
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                    // REDIRECCIÓN
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
                if (model.Password != ConfirmPassword)
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
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult RecuperarPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Ingresa tu correo electrónico.");
                return View();
            }

            var token = _usuarioService.GenerarTokenRecuperacion(email);

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "No existe una cuenta con ese correo.");
                return View();
            }

            var enlace = Url.Action(
                "RestablecerPassword",
                "Account",
                new { token = token },
                Request.Scheme);

            ViewBag.EnlaceRecuperacion = enlace;
            ViewBag.Mensaje = "Se generó el enlace de recuperación.";

            return View();
        }

        [HttpGet]
        public IActionResult RestablecerPassword(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return RedirectToAction("Login");

            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RestablecerPassword(
            string token,
            string nuevaPassword,
            string confirmarPassword)
        {
            var mensaje = _usuarioService.RestablecerPassword(
                token,
                nuevaPassword,
                confirmarPassword);

            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Token = token;
                ModelState.AddModelError("", mensaje);
                return View();
            }

            TempData["Mensaje"] = "Contraseña actualizada correctamente. Ahora puedes iniciar sesión.";
            return RedirectToAction("Login");
        }
    }
}