using G3_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G3_Practice.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VentasController : Controller
    {

        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var ventas = _context.Ventas.ToList();
            return View(ventas);
        }
    }
}