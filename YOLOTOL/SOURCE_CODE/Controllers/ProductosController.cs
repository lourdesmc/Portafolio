using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YOLOTOL.Data;
using YOLOTOL.Helpers;
using YOLOTOL.Models;

namespace YOLOTOL.Controllers
{
    public class ProductosController : Controller
    {
        private readonly YolotolContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public ProductosController(YolotolContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;

        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            String tipoUsuario = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "tipoUsuario");
            if (User.Identity.IsAuthenticated)
            {
                if (tipoUsuario == "Administrador")
                {
                    return View(await _context.Producto.ToListAsync());
                }
            }
            return RedirectToAction("Error", "Usuarios");
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Producto
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Archivo,Precio,Stock,Descripcion,IdCategoria")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                if (productos.Archivo != null)
                {
                    // Guardar la imagen en wwwroot/imagenes
                    string rutaCarpeta = webHostEnvironment.WebRootPath;
                    string nombreArchivo = Path.GetFileNameWithoutExtension(productos.Archivo.FileName);
                    string extension = Path.GetExtension(productos.Archivo.FileName);
                    //Asignar al atributo
                    productos.Imagen = nombreArchivo = 
                        nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(rutaCarpeta + "/Producto/", nombreArchivo);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await productos.Archivo.CopyToAsync(fileStream);
                    }
                    _context.Add(productos);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("ErrorProductos", "Debe seleccionar una imagen");
                    ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre");
                    return View(productos);
                }
            }
            ModelState.AddModelError("ErrorProductos", "Favor de revisar los datos");
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre");
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Producto.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Archivo,Precio,Stock,Descripcion,IdCategoria")] Productos productos)
        {
            if (id != productos.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (productos.Archivo != null)
                    {
                        //Guardar la imagen en wwwroot/imagenes
                        string rutaCarpeta = webHostEnvironment.WebRootPath;
                        string nombreArchivo = Path.GetFileNameWithoutExtension(productos.Archivo.FileName);
                        string extension = Path.GetExtension(productos.Archivo.FileName);
                        //Asignar al atributo
                        productos.Imagen = nombreArchivo = nombreArchivo + DateTime.Now.ToString("yymmssfff") + extension;

                        string path = Path.Combine(rutaCarpeta + "/Producto/", nombreArchivo);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await productos.Archivo.CopyToAsync(fileStream);
                        }
                        _context.Update(productos);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.IdProducto))
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
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Producto
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Producto.FindAsync(id);
            _context.Producto.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Producto.Any(e => e.IdProducto == id);
        }
    }
}
