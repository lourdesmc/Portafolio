using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using YOLOTOL.Data;
using YOLOTOL.Models;

namespace YOLOTOL.Controllers
{
    public class HomeController : Controller
    {
        private readonly YolotolContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, YolotolContext context)
        {
            _context = context;

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inicio()
        {
            return View();
        }
        public IActionResult Nosotros()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            return View();
        }
        public IActionResult Descargar()
        {
            return View();
        }
        
        public IActionResult InicioSesion()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
