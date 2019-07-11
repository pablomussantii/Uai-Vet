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
    public class FacturaProductoController : Controller
    {
        private VetDbContext db = new VetDbContext();

        // GET: FacturaProducto
        public ActionResult Index()
        {
            var facturaProductos = db.FacturaProductos.Include(f => f.Cliente);
            return View(facturaProductos.ToList());
        }

        // GET: FacturaProducto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaProducto facturaProducto = db.FacturaProductos.Find(id);
            if (facturaProducto == null)
            {
                return HttpNotFound();
            }
            return View(facturaProducto);
        }

        // GET: FacturaProducto/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto");
            return View();
        }

        // POST: FacturaProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,IdCliente,Monto")] FacturaProducto facturaProducto)
        {
            if (ModelState.IsValid)
            {
                db.FacturaProductos.Add(facturaProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaProducto.IdCliente);
            return View(facturaProducto);
        }

        // GET: FacturaProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaProducto facturaProducto = db.FacturaProductos.Find(id);
            if (facturaProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaProducto.IdCliente);
            return View(facturaProducto);
        }

        // POST: FacturaProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,IdCliente,Monto")] FacturaProducto facturaProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturaProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaProducto.IdCliente);
            return View(facturaProducto);
        }

        // GET: FacturaProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaProducto facturaProducto = db.FacturaProductos.Find(id);
            if (facturaProducto == null)
            {
                return HttpNotFound();
            }
            return View(facturaProducto);
        }

        // POST: FacturaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacturaProducto facturaProducto = db.FacturaProductos.Find(id);
            db.FacturaProductos.Remove(facturaProducto);
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
