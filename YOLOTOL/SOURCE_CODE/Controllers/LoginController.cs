using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YOLOTOL.Data;
using YOLOTOL.Helpers;

namespace YOLOTOL.Controllers
{
    public class LoginController : Controller
    {
        private readonly YolotolContext context;

        public LoginController(YolotolContext _context)
        {           
            this.context = _context;
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            //ViewData["IdTipoDeUsuario"] = new SelectList(context.TipoDeUsuario, "IdTipoDeUsuario", "Nombre");
            return View();
        }
        public async Task<IActionResult> Login(Models.Usuarios usu)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)

              .Select(x => new { x.Key, x.Value.Errors })

              .ToArray();
            if (ModelState.IsValid)
            {
                var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.correo == usu.correo
                        && u.contrasenia == usu.contrasenia);

                if (usuario == null)
                {
                    ModelState.AddModelError("ErrorLogin", "Las credenciales no son válidas");
                    return View("../Home/Login", usu);
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.correo),
                        new Claim(ClaimTypes.Role, usuario.tipoUsuario),
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);



                    var tipoUsuario = usuario.tipoUsuario;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "tipoUsuario", tipoUsuario);

                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                ModelState.AddModelError("ErrorLogin", "Los campos son obligatorios");
                return View("../Home/Login", usu);
            }

        }

    }
}
