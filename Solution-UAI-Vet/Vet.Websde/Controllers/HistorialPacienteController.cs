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
    public class HistorialPacienteController : Controller
    {
        private VetDbContext db = new VetDbContext();
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(HistorialPacienteController));

        // GET: HistorialPaciente
        public ActionResult Index()
        {
            var historialPacientes = db.HistorialPacientes.Include(h => h.Paciente);
            return View(historialPacientes.ToList());
        }

        // GET: HistorialPaciente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPaciente historialPaciente = db.HistorialPacientes.Find(id);
            if (historialPaciente == null)
            {
                return HttpNotFound();
            }
            return View(historialPaciente);
        }

        // GET: HistorialPaciente/Create
        public ActionResult Create()
        {
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre");
            return View();
        }

        // POST: HistorialPaciente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPaciente,Descripcion,Fecha")] HistorialPaciente historialPaciente)
        {
            if (ModelState.IsValid)
            {
                historialPaciente.Fecha = DateTime.Now;
                db.HistorialPacientes.Add(historialPaciente);
                db.SaveChanges();
                log.Info("Creacion de historial de paciente");
                return RedirectToAction("Index");
            }

            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", historialPaciente.IdPaciente);
            return View(historialPaciente);
        }

        // GET: HistorialPaciente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPaciente historialPaciente = db.HistorialPacientes.Find(id);
            if (historialPaciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", historialPaciente.IdPaciente);
            return View(historialPaciente);
        }

        // POST: HistorialPaciente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPaciente,Descripcion,Fecha")] HistorialPaciente historialPaciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historialPaciente).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("Edicion de historial de paciente");
                return RedirectToAction("Index");
            }
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", historialPaciente.IdPaciente);
            return View(historialPaciente);
        }

        // GET: HistorialPaciente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPaciente historialPaciente = db.HistorialPacientes.Find(id);
            if (historialPaciente == null)
            {
                return HttpNotFound();
            }
            return View(historialPaciente);
        }

        // POST: HistorialPaciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialPaciente historialPaciente = db.HistorialPacientes.Find(id);
            db.HistorialPacientes.Remove(historialPaciente);
            db.SaveChanges();
            log.Info("Eliminacion de historial de paciente");
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
