using G3_Practice.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G3_Practice.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {

        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Carrito
        public ActionResult Index()
        {
            List<CarritoDetalle> carrito = Session["Carrito"] as List<CarritoDetalle> ?? new List<CarritoDetalle>();
            return View(carrito);
        }

        [HttpGet]
        public ActionResult AgregarAlCarrito(int id, string productoName, decimal price, int stock, string imagen)
        {
            List<CarritoDetalle> carrito = Session["Carrito"] as List<CarritoDetalle> ?? new List<CarritoDetalle>();
            var productoExistente = carrito.FirstOrDefault(p => p.ProuctoId == id);
            if (productoExistente != null)
            {
                productoExistente.Cantiad++;
                productoExistente.TotalPrice = productoExistente.Price * productoExistente.Cantiad;
                if (productoExistente.Cantiad > stock)
                {
                    return RedirectToAction("CantidadMayorAStockIndex", "Carrito");
                }
            }
            else
            {
                CarritoDetalle nuevoProducto = new CarritoDetalle
                {
                    ProuctoId = id,
                    ProductoName = productoName,
                    Price = (float?)price,
                    stock = stock,
                    imagen = imagen,
                    Cantiad = 1,
                    TotalPrice = (float?)price * 1
                };
                carrito.Add(nuevoProducto);
            }
            decimal totalCarrito = carrito.Sum(p => Convert.ToDecimal(p.TotalPrice));
            Session["Carrito"] = carrito;
            Session["TotalCarrito"] = totalCarrito;
            return RedirectToAction("Index", "Carrito");
        }

        [HttpGet]
        public ActionResult AumetarLaCantidad(int id, int stock)
        {
            List<CarritoDetalle> carrito = Session["Carrito"] as List<CarritoDetalle> ?? new List<CarritoDetalle>();
            var productoExistente = carrito.FirstOrDefault(p => p.ProuctoId == id);
            if (productoExistente != null)
            {
                productoExistente.Cantiad++;
                if (productoExistente.Cantiad > stock)
                {
                    return RedirectToAction("CantidadMayorAStock2Index", "Carrito");
                }
                productoExistente.TotalPrice = productoExistente.Price * productoExistente.Cantiad;
                decimal totalCarrito = carrito.Sum(p => Convert.ToDecimal(p.TotalPrice));
                Session["Carrito"] = carrito;
                Session["TotalCarrito"] = totalCarrito;
            }
            return RedirectToAction("Index", "Carrito");
        }


        [HttpGet]
        public ActionResult ReducirLaCantidad(int id, int stock)
        {
            List<CarritoDetalle> carrito = Session["Carrito"] as List<CarritoDetalle> ?? new List<CarritoDetalle>();
            var productoExistente = carrito.FirstOrDefault(p => p.ProuctoId == id);
            if (productoExistente != null)
            {
                productoExistente.Cantiad--;
                if (productoExistente.Cantiad < 1)
                {
                    carrito.Remove(productoExistente);
                    return RedirectToAction("Index", "Carrito");
                }
                productoExistente.TotalPrice = productoExistente.Price * productoExistente.Cantiad;
                decimal totalCarrito = carrito.Sum(p => Convert.ToDecimal(p.TotalPrice));
                Session["Carrito"] = carrito;
                Session["TotalCarrito"] = totalCarrito;
            }
            return RedirectToAction("Index", "Carrito");
        }

        [HttpGet]
        public ActionResult EliminarProducto(int id)
        {
            List<CarritoDetalle> carrito = Session["Carrito"] as List<CarritoDetalle> ?? new List<CarritoDetalle>();
            var productoExistente = carrito.FirstOrDefault(p => p.ProuctoId == id);
            if (productoExistente != null)
            {
                carrito.Remove(productoExistente);
                decimal totalCarrito = carrito.Sum(p => Convert.ToDecimal(p.TotalPrice));
                Session["Carrito"] = carrito;
                Session["TotalCarrito"] = totalCarrito;
            }
            return RedirectToAction("Index", "Carrito");
        }


        public ActionResult CantidadMayorAStockIndex() { 
            return View();
        }
        
        public ActionResult CantidadMayorAStock2Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FinalizarCompra()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinalizarCompra(string direccion, string metodoPago)
        {
            List<CarritoDetalle> carrito = Session["Carrito"] as List<CarritoDetalle> ?? new List<CarritoDetalle>();

            if (carrito.Count == 0)
            {
                return RedirectToAction("Index", "Carrito");
            }
            string userId = User.Identity.GetUserId();
            DateTime fechaPedido = DateTime.Now;
            float totalCarrito = carrito.Sum(p => (float)p.TotalPrice);
            var nuevoPedido = new Pedidos
            {
                UsuarioId = userId,
                FechaPedido = fechaPedido,
                EstadoId = 1,
                DireccionEntrega = direccion,
                MetodoPago = metodoPago,
                total = totalCarrito
            };

            context.Pedidos.Add(nuevoPedido);
            context.SaveChanges();

            int pedidoId = nuevoPedido.PedidoId;
            foreach (var item in carrito)
            {
                var producto = context.Productos.FirstOrDefault(p => p.ProductoId == item.ProuctoId);
                if (producto != null)
                {
                    producto.stock -= item.Cantiad;
                }

                var detallePedido = new PedidoDetalles
                {
                    PedidoId = pedidoId,
                    ProductoId = item.ProuctoId,
                    Cantidad = item.Cantiad,
                    Precio = (float)item.Price,
                    SubTotal = (float)item.TotalPrice
                };
                context.PedidoDetalles.Add(detallePedido);
            }
            context.SaveChanges();

            var venta = new Ventas
            {
                PedidoId = pedidoId,
                FechaVenta = fechaPedido,
                MetodoPago = metodoPago,
                TotalVenta = totalCarrito
            };
            context.Ventas.Add(venta);
            context.SaveChanges();

            Session["Carrito"] = null;
            Session["TotalCarrito"] = null;

            return RedirectToAction("CompraExitosa", "Carrito");
        }

        
        public ActionResult CompraExitosa()
        {
            return View();
        }
    }
}