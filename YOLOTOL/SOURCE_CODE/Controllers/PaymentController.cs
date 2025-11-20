using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOLOTOL.Helpers;
using YOLOTOL.Models;
using YOLOTOL.Services;

namespace YOLOTOL.Controllers
{
    public class PaymentController : Controller
    {
        #region Single Paypal Payment
        public IActionResult CreatePayment()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");//Obtener la lista
            var payment = PayPalPaymentService.CreatePayment(GetBaseUrl(), "sale", cart);
            return Redirect(payment.GetApprovalUrl());
        }

        public IActionResult PaymentSuccessful(string paymentId, string token, string PayerId)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var payment = PayPalPaymentService.ExecutePayment(paymentId, PayerId);
            if (cart != null)
            {
                ViewBag.cart = cart;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
            //Info del cliente
            Payer payer = payment.payer;
            ViewBag.cliente = payer.payer_info;
            //Dirección del cliente
            ViewBag.direccion = payer.payer_info.shipping_address;
            //Detalles de la transaccion
            List<Transaction> transactions = payment.transactions;
            foreach (var detail in transactions)
            {
                ViewBag.importe = detail.amount;
                ViewBag.detalles = detail.amount.details;
                ViewBag.articulos = detail.item_list.items;
            }
            return View();
        }
        #endregion 

        public string GetBaseUrl()
        {
            return Request.Scheme + "://" + Request.Host;
        }
    }

}
