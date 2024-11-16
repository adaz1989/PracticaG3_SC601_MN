using G3_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G3_Practice.Controllers
{
    public class ProductoController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductosClientesIndex()
        {
            var productos = context.Productos.ToList();
            return View(productos);
        }
    }
}