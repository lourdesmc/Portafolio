using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YOLOTOL.Data;
using YOLOTOL.Helpers;
using YOLOTOL.Models;

namespace YOLOTOL.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly YolotolContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UsuariosController(YolotolContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            String tipoUsuario = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "tipoUsuario");
            if (User.Identity.IsAuthenticated)
            {
                if (tipoUsuario == "Administrador")
                {
                    return View(await _context.Usuario.ToListAsync());
                }
            }
            return RedirectToAction("Error", "Usuarios");
        }
        public IActionResult Error()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuario
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        //Registro
        [HttpPost]
        public async Task<IActionResult> Add([Bind("IdUsuario,nombre,apellidoP,apellidoM,correo,contrasenia,fechaNacimiento,archivo,cedula,telefono,tipoUsuario")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if (usuarios.archivo != null)
                {
                    // Guardar la imagen en wwwroot/imagenes
                    string rutaCarpeta = webHostEnvironment.WebRootPath;
                    string nombreArchivo = Path.GetFileNameWithoutExtension(usuarios.archivo.FileName);
                    string extension = Path.GetExtension(usuarios.archivo.FileName);
                    //Asignar al atributo
                    usuarios.fotoPerfil = nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(rutaCarpeta + "/FotoPerfil/", nombreArchivo);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await usuarios.archivo.CopyToAsync(fileStream);
                    }
                    _context.Add(usuarios);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Login", "Home");

                }
            }
            return RedirectToAction("Registro", "Home");
            //return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,nombre,apellidoP,apellidoM,correo,contrasenia,fechaNacimiento,archivo,cedula,telefono,tipoUsuario")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                if(usuarios.archivo != null)
                {
                    // Guardar la imagen en wwwroot/imagenes
                    string rutaCarpeta = webHostEnvironment.WebRootPath;
                    string nombreArchivo = Path.GetFileNameWithoutExtension(usuarios.archivo.FileName);
                    string extension = Path.GetExtension(usuarios.archivo.FileName);
                    //Asignar al atributo
                    usuarios.fotoPerfil = nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(rutaCarpeta + "fotoPerfil/", nombreArchivo);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await usuarios.archivo.CopyToAsync(fileStream);
                    }
                    _context.Add(usuarios);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuario.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,nombre,apellidoP,apellidoM,correo,contrasenia,fechaNacimiento,archivo,cedula,telefono,tipoUsuario")] Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(usuarios.archivo != null)
                    {
                        // Guardar la imagen en wwwroot/imagenes
                        string rutaCarpeta = webHostEnvironment.WebRootPath;
                        string nombreArchivo = Path.GetFileNameWithoutExtension(usuarios.archivo.FileName);
                        string extension = Path.GetExtension(usuarios.archivo.FileName);
                        //Asignar al atributo
                        usuarios.fotoPerfil = nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;

                        string path = Path.Combine(rutaCarpeta + "/fotoPerfil/", nombreArchivo);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await usuarios.archivo.CopyToAsync(fileStream);
                        }
                    }
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuario
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuario.Any(e => e.IdUsuario == id);
        }
    }
}
