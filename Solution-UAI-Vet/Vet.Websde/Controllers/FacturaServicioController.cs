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
    public class FacturaServicioController : Controller
    {
        private VetDbContext db = new VetDbContext();

        // GET: FacturaServicio
        public ActionResult Index()
        {
            var facturaServicios = db.FacturaServicios.Include(f => f.Cliente);
            return View(facturaServicios.ToList());
        }

        // GET: FacturaServicio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaServicio facturaServicio = db.FacturaServicios.Find(id);
            if (facturaServicio == null)
            {
                return HttpNotFound();
            }
            return View(facturaServicio);
        }

        // GET: FacturaServicio/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto");
            return View();
        }

        // POST: FacturaServicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoEspecialidad,Fecha,IdCliente,Monto")] FacturaServicio facturaServicio)
        {
            if (ModelState.IsValid)
            {
                db.FacturaServicios.Add(facturaServicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaServicio.IdCliente);
            return View(facturaServicio);
        }

        // GET: FacturaServicio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaServicio facturaServicio = db.FacturaServicios.Find(id);
            if (facturaServicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaServicio.IdCliente);
            return View(facturaServicio);
        }

        // POST: FacturaServicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoEspecialidad,Fecha,IdCliente,Monto")] FacturaServicio facturaServicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturaServicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "NombreCompleto", facturaServicio.IdCliente);
            return View(facturaServicio);
        }

        // GET: FacturaServicio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaServicio facturaServicio = db.FacturaServicios.Find(id);
            if (facturaServicio == null)
            {
                return HttpNotFound();
            }
            return View(facturaServicio);
        }

        // POST: FacturaServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacturaServicio facturaServicio = db.FacturaServicios.Find(id);
            db.FacturaServicios.Remove(facturaServicio);
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
