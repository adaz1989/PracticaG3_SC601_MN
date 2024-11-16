using G3_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace G3_Practice.Controllers
{
    public class PreferenciasAlimenticiasController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        // GET: PreferenciasAlimenticias
        [HttpGet]
        public ActionResult Index()
        {
            var Preferencias = _context.PreferenciasAlimenticias.ToList();
            return View(Preferencias);
        }

        //=====================================================================================
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(PreferenciaAlimenticia preferenciaAlimenticia)
        {
            if (ModelState.IsValid)
            {
                _context.PreferenciasAlimenticias.Add(preferenciaAlimenticia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preferenciaAlimenticia);
        }
        //=====================================================================================

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var preferenciaAlimenticia = _context.PreferenciasAlimenticias.Find(id);
            if (preferenciaAlimenticia == null)
                return HttpNotFound();
            return View(preferenciaAlimenticia);
        }

        [HttpPost]
        public ActionResult Editar(PreferenciaAlimenticia preferenciaAlimenticia)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(preferenciaAlimenticia).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preferenciaAlimenticia);
        }
//=====================================================================================

        [HttpGet]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var preferenciaAlimenticia = _context.PreferenciasAlimenticias.Find(id);
            if (preferenciaAlimenticia == null)
                return HttpNotFound();

            return View(preferenciaAlimenticia);
        }
        //=====================================================================================

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var PreferenciaAlimenticia = _context.PreferenciasAlimenticias.SingleOrDefault(l => l.Id == id);
            if (PreferenciaAlimenticia == null)
                return HttpNotFound();
            return View(PreferenciaAlimenticia);
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult EliminarConfirmacion(int? id)
        {
            var libro = _context.PreferenciasAlimenticias.Find(id);
            _context.PreferenciasAlimenticias.Remove(libro);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}