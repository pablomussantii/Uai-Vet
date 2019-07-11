using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vet.Data;
using Vet.Domain;

namespace Vet.Websde.Controllers
{
    public class DetalleFacturaProductoController : Controller
    {
        private VetDbContext db = new VetDbContext();

        // GET: DetalleFacturaProducto
        public ActionResult Index()
        {
            var detalleFacturaProductos = db.DetalleFacturaProductos.Include(d => d.FacturaProducto).Include(d => d.Producto);
            return View(detalleFacturaProductos.ToList());
        }

        // GET: DetalleFacturaProducto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFacturaProducto detalleFacturaProducto = db.DetalleFacturaProductos.Find(id);
            if (detalleFacturaProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleFacturaProducto);
        }

        // GET: DetalleFacturaProducto/Create
        public ActionResult Create()
        {
            ViewBag.IdFacturaProducto = new SelectList(db.FacturaProductos, "Id", "Id");
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre");
            return View();
        }

        // POST: DetalleFacturaProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFacturaProducto,IdProducto,Cantidad")] DetalleFacturaProducto detalleFacturaProducto)
        {
            if (ModelState.IsValid)
            {
                db.DetalleFacturaProductos.Add(detalleFacturaProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFacturaProducto = new SelectList(db.FacturaProductos, "Id", "Id", detalleFacturaProducto.IdFacturaProducto);
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", detalleFacturaProducto.IdProducto);
            return View(detalleFacturaProducto);
        }

        // GET: DetalleFacturaProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFacturaProducto detalleFacturaProducto = db.DetalleFacturaProductos.Find(id);
            if (detalleFacturaProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFacturaProducto = new SelectList(db.FacturaProductos, "Id", "Id", detalleFacturaProducto.IdFacturaProducto);
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", detalleFacturaProducto.IdProducto);
            return View(detalleFacturaProducto);
        }

        // POST: DetalleFacturaProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFacturaProducto,IdProducto,Cantidad")] DetalleFacturaProducto detalleFacturaProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleFacturaProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFacturaProducto = new SelectList(db.FacturaProductos, "Id", "Id", detalleFacturaProducto.IdFacturaProducto);
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", detalleFacturaProducto.IdProducto);
            return View(detalleFacturaProducto);
        }

        // GET: DetalleFacturaProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleFacturaProducto detalleFacturaProducto = db.DetalleFacturaProductos.Find(id);
            if (detalleFacturaProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleFacturaProducto);
        }

        // POST: DetalleFacturaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleFacturaProducto detalleFacturaProducto = db.DetalleFacturaProductos.Find(id);
            db.DetalleFacturaProductos.Remove(detalleFacturaProducto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
