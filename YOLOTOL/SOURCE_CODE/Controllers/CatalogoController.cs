using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOLOTOL.Data;
using YOLOTOL.Helpers;
using YOLOTOL.Models;

namespace YOLOTOL.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly YolotolContext _context;
        public CatalogoController(YolotolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");//Obtener la lista
            if (cart != null)
            {
                ViewBag.count = cart.Count;//Contando el número de elementos en el carrito
                ViewBag.cart = cart;//Elementos del carrito
                ViewBag.total = cart.Sum(item => item.Producto.Precio * item.Quantity);//calCULO total = x cada elemento del cart presio*cantidad
                ViewBag.cantidad = cart.Sum(item => item.Quantity);//Calculo cantidad
            }
            return View(await _context.Producto.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Producto.FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");//Obtener la lista
            if (cart != null)
            {
                ViewBag.count = cart.Count;//Contando el número de elementos en el carrito
                ViewBag.cart = cart;//Elementos del carrito
                ViewBag.total = cart.Sum(item => item.Producto.Precio * item.Quantity);//calCULO total = x cada elemento del cart presio*cantidad
                ViewBag.cantidad = cart.Sum(item => item.Quantity);//Calculo cantidad
            }
            return View(await _context.Producto.ToListAsync());
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            Productos productos = new Productos();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();//Crear variable(carrito)
                var producto = await _context.Producto.FirstOrDefaultAsync(m => m.IdProducto == id);//Busca el producto y lo guarda
                cart.Add(new CartItem { Producto = producto, Quantity = 1 });//Agrega el elemento al carrito, es decir el producto o la cantidad
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);//El elemento del carrito lo guarda en la sesión
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    var producto = await _context.Producto.FirstOrDefaultAsync(m => m.IdProducto == id);//Busca el producto y lo guarda
                    cart.Add(new CartItem { Producto = producto, Quantity = 1 });//Agrega el elemento al carrito, es decir el producto o la cantidad
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);//El elemento del carrito lo guarda en la sesión

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, int quantity)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");//Obtener la lista
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity = quantity;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("ShoppingCart");

        }

        public async Task<IActionResult> Remove(int? id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");//Obtener la lista
            int index = isExist(id);
            if (index != -1 && cart.Count > 1)
            {
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else if (index != -1 && cart.Count == 1)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
            }
            return RedirectToAction("ShoppingCart");

        }

        private int isExist(int? id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Producto.IdProducto.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
