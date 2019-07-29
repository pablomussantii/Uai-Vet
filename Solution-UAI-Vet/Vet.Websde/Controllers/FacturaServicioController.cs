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
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(FacturaServicioController));

        // GET: FacturaServicio
        public ActionResult Index()
        {
            var facturaServicios = db.FacturaServicios.Include(f => f.Turno);
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
            RepositoryTurno repositoryturno = new RepositoryTurno();
            List<Turno> lstturno = new List<Turno>();
            foreach (var item in repositoryturno.List())
            {
                if (item.Abonado == false)
                {
                    lstturno.Add(item);
                }
            }
            ViewBag.IdTurno = new SelectList(lstturno, "Id", "Id");
            return View();
        }

        // POST: FacturaServicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdTurno,Fecha,Monto")] FacturaServicio facturaServicio)
        {
            if (ModelState.IsValid)
            {
                facturaServicio.Fecha = DateTime.Now;
                RepositoryTurno repositoryturno = new RepositoryTurno();
                Turno modelturno = new Turno();
                modelturno = repositoryturno.GetById(facturaServicio.IdTurno);
                modelturno.Abonado = true;
                repositoryturno.Update(modelturno);

                db.FacturaServicios.Add(facturaServicio);
                db.SaveChanges();
                log.Info("Creacion de Factura servicio");
                return RedirectToAction("Index");
            }

            ViewBag.IdTurno = new SelectList(db.Turnos, "Id", "Id", facturaServicio.IdTurno);
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
            ViewBag.IdTurno = new SelectList(db.Turnos, "Id", "Id", facturaServicio.IdTurno);
            return View(facturaServicio);
        }

        // POST: FacturaServicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTurno,Fecha,Monto")] FacturaServicio facturaServicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturaServicio).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("Edicion de Factura servicio");
                return RedirectToAction("Index");
            }
            ViewBag.IdTurno = new SelectList(db.Turnos, "Id", "Id", facturaServicio.IdTurno);
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
            log.Info("Eliminacion de Factura servicio");
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
