using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOLOTOL.Models;

namespace YOLOTOL.Services
{
    public class PayPalPaymentService
    {
        public static Payment CreatePayment(string baseUrl, string intent, List<CartItem> cart)
        {
            var apiContext = PayPalConfiguration.GetAPIContext();
            var payment = new Payment()
            {
                intent = intent,
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(cart),
                redirect_urls = GetReturnUrls(baseUrl, intent)
            };

            var createPatment = payment.Create(apiContext);
            return createPatment;
        }


        private static List<Transaction> GetTransactionsList(List<CartItem> cart)
        {
            decimal subt = 0;
            decimal tasaImpuesto = 20;
            decimal envio = 30;
            decimal totalProducto = 0;

            List<Item> items = new List<Item>();//Lista de articulos de PayPal

            foreach (var item in cart)//Recorrer mi carrito de sesión
            {
                items.Add(//Agregar cada articulo a la lista

            new Item()//Nuevo articulo de PayPal
            {
                name = item.Producto.Nombre,
                currency = "MXN",
                price = item.Producto.Precio.ToString(),
                quantity = item.Quantity.ToString(),
                sku = item.Producto.IdProducto.ToString(),

            });
                totalProducto = item.Quantity * item.Producto.Precio;//Calculo el total por producto
                subt += totalProducto;
            }

            ItemList itemList = new ItemList();
            itemList.items = items;//Guardo mi lista de artículos

            var details = new Details()//Detalles de la compra: tasa de impueso, envío y subtotal
            {
                tax = tasaImpuesto.ToString(),
                shipping = envio.ToString(),
                subtotal = subt.ToString()
            };
            decimal total = tasaImpuesto + envio + subt;
            var amount = new Amount
            {
                currency = "MXN",
                total = total.ToString(),
                details = details
            };
            var transactionList = new List<Transaction>();
            transactionList.Add(
                new Transaction()
                {
                    description = "Compra en Vívero.com",
                    invoice_number = GetRandomInvoiceNumber(),
                    amount = amount,
                    item_list = itemList
                }
            );
            return transactionList;
        }
        //ExecutePAyment
        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            Console.WriteLine("ExecutePayment");
            var apiContext = PayPalConfiguration.GetAPIContext();

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };


            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
        //
        private static RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            Console.WriteLine("GetReturnUrls");
            var returnUrl = intent == "sale" ? "/Payment/PaymentSuccessful" : "/Home/AuthorizeSuccessful";


            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/Home/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
    }

}
