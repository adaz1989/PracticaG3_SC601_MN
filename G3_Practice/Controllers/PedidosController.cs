using G3_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G3_Practice.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PedidosController : Controller
    {
        // GET: Pedidos
        private ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var Pedidos = context.Pedidos.ToList();
            return View(Pedidos);
        }

        [HttpGet]
        public ActionResult Detalles(int? Id)
        {
            if (Id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);        

            var productosDelPedido = context.PedidoDetalles
                                     .Where(p => p.PedidoId == Id)
                                     .ToList();

            if (productosDelPedido == null) return HttpNotFound();

            return View(productosDelPedido);
            
        }

        [HttpGet]
        public ActionResult Editar(int? Id) 
        {
            if (Id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var pedido = context.Pedidos.Find(Id);
            if (pedido == null)            
                return HttpNotFound();
            ViewBag.Estados = new SelectList(context.EstadoPedidoes, "EstadoId", "Descripcion", pedido.PedidoId);
            return View(pedido);
        }

        [HttpPost]
        public ActionResult Editar(Pedidos pedido)
        {
            if (ModelState.IsValid)
            {                
                var pedidoExistente = context.Pedidos.Find(pedido.PedidoId);

                if (pedidoExistente != null)
                {                    
                    pedidoExistente.EstadoId = pedido.EstadoId;
                    
                    context.Entry(pedidoExistente).State = System.Data.Entity.EntityState.Modified;
                   
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(pedido);
        }

    }
}