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
                    model.PasswordHash);

                if (usuario != null)
                {
<<<<<<< HEAD
                    //Guardar sesion
                    HttpContext.Session.SetString("Usuario", usuario.Nombre);
                    HttpContext.Session.SetString("Rol", usuario.Rol);
=======
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
>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
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
<<<<<<< HEAD
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
=======
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

>>>>>>> 7b1daec6bb3082888fd86b4fca5b639a6b80a4d6
            return RedirectToAction("Login");
        }
    }
}