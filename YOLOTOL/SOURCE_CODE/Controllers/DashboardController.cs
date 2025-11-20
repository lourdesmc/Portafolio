using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOLOTOL.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Datos()
        {
            return View();
        }
        public IActionResult Perfil()
        {
            return View();
        }
        public IActionResult Logros()
        {
            return View();
        }
    }
}
