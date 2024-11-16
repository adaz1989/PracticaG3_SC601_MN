using G3_Practice.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace G3_Practice.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_context.Productos.ToList());
        }

        public ActionResult Create()
        {
            var preferencias = _context.PreferenciasAlimenticias.ToList();
            if (preferencias == null || !preferencias.Any())
            {
                TempData["Error"] = "No hay preferencias alimenticias disponibles.";
            }
            ViewBag.Preferencias = preferencias;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Productos producto, int[] PreferenciasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();

                if (PreferenciasSeleccionadas != null && PreferenciasSeleccionadas.Any())
                {
                    foreach (var preferenciaId in PreferenciasSeleccionadas)
                    {
                        var productoPreferencia = new ProductoPreferencia
                        {
                            ProductoId = producto.ProductoId,
                            PreferenciaAlimenticiaId = preferenciaId
                        };
                        _context.ProductoPreferencia.Add(productoPreferencia);
                    }
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Preferencias = _context.PreferenciasAlimenticias.ToList();
            return View(producto);
        }

        public ActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            var preferenciasSeleccionadas = _context.ProductoPreferencia
                                                      .Where(pp => pp.ProductoId == producto.ProductoId)
                                                      .Select(pp => pp.PreferenciaAlimenticiaId)
                                                      .ToList();

            var preferencias = _context.PreferenciasAlimenticias.ToList();
            if (preferencias == null || !preferencias.Any())
            {
                TempData["Error"] = "No hay preferencias alimenticias disponibles.";
                ViewBag.Preferencias = new List<G3_Practice.Models.PreferenciaAlimenticia>(); // Asignar lista vacía
            }
            else
            {
                ViewBag.Preferencias = preferencias;
            }

            ViewBag.PreferenciasSeleccionadas = preferenciasSeleccionadas ?? new List<int>();

            return View(producto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Productos producto, int[] PreferenciasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(producto).State = EntityState.Modified;
                _context.SaveChanges();

                var preferenciasAntiguas = _context.ProductoPreferencia
                                                    .Where(pp => pp.ProductoId == producto.ProductoId)
                                                    .ToList();
                _context.ProductoPreferencia.RemoveRange(preferenciasAntiguas);

                if (PreferenciasSeleccionadas != null && PreferenciasSeleccionadas.Any())
                {
                    foreach (var preferenciaId in PreferenciasSeleccionadas)
                    {
                        var productoPreferencia = new ProductoPreferencia
                        {
                            ProductoId = producto.ProductoId,
                            PreferenciaAlimenticiaId = preferenciaId
                        };
                        _context.ProductoPreferencia.Add(productoPreferencia);
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Preferencias = _context.PreferenciasAlimenticias.ToList();
            return View(producto);
        }

        [HttpPost]
        public ActionResult RemoveProducto(int productoId)
        {
            var producto = _context.Productos.Find(productoId);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();

                TempData["Message"] = "Producto eliminado correctamente.";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "No se pudo encontrar el producto.";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
