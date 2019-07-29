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
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(FacturaProductoController));

        // GET: FacturaProducto
        public ActionResult Index()
        {
            var facturaProductos = db.FacturaProductos.Include(f => f.Cliente).Include(f => f.Producto);
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
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre");
            return View();
        }

        // POST: FacturaProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdCliente,IdProducto,Cantidad,Fecha,Monto")] FacturaProducto facturaProducto)
        {
            if (ModelState.IsValid)
            {
                facturaProducto.Fecha = DateTime.Now;
                RepositoryProducto repositoryProducto = new RepositoryProducto();
                Producto prod = new Producto();
                prod = repositoryProducto.GetById(facturaProducto.IdProducto);
                facturaProducto.Monto = prod.Precio * facturaProducto.Cantidad;
                db.FacturaProductos.Add(facturaProducto);
                db.SaveChanges();
                log.Info("Creacion de factura producto");
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaProducto.IdCliente);
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", facturaProducto.IdProducto);
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
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", facturaProducto.IdProducto);
            return View(facturaProducto);
        }

        // POST: FacturaProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCliente,IdProducto,Cantidad,Fecha,Monto")] FacturaProducto facturaProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturaProducto).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("Edicion de factura producto");
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaProducto.IdCliente);
            ViewBag.IdProducto = new SelectList(db.Productos, "Id", "Nombre", facturaProducto.IdProducto);
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
            log.Info("Eliminacion de factura producto");
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
