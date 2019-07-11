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
    public class TurnoController : Controller
    {
        private VetDbContext db = new VetDbContext();

        // GET: Turno
        public ActionResult Index()
        {
            RepositoryDoctor repositoryDoctor = new RepositoryDoctor();

            var turnos = db.Turnos.Include(t => t.Atencion).Include(t => t.Paciente);

            List<Turno> tur = new List<Turno>();
            tur = turnos.ToList();

            IEnumerable<Turno> lstordenada = tur.OrderBy(User => User.Fecha);

            foreach (var item in tur)
            {

            }

            return View(tur);
        }

        // GET: Turno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // GET: Turno/Create
        public ActionResult Create()
        {
            RepositoryAtencion repositoryatencion = new RepositoryAtencion();
           ViewBag.atenciones= repositoryatencion.List();
            ViewBag.IdAtencion = new SelectList(db.Atenciones, "Id", "Id");
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre");
            return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoEspecialidad,Fecha,IdPaciente,IdAtencion,Hora,Abonado")] Turno turno)
        {
            List<Atencion> lstatencion = new List<Atencion>();
            if (turno.IdAtencion == 0)
            {
                RepositoryAtencion repositoryatencion = new RepositoryAtencion();
               
                foreach (var item in repositoryatencion.List())
                {
                    if (item.TipoEspecialidad == turno.TipoEspecialidad)
                    {
                        lstatencion.Add(item);
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {

                    RepositoryAtencion repositoryatencion = new RepositoryAtencion();
                    RepositoryTurno repositoryTurno = new RepositoryTurno();
                    Atencion natencion = new Atencion();
                    natencion = repositoryatencion.GetById(turno.IdAtencion);
                    int valor = 0;
                    ViewBag.advertencia2 = "No corresponde la atencion con lo que se solicita";
                    foreach (var item in repositoryTurno.List())
                    {
                        if (item.Fecha == turno.Fecha && item.Hora == turno.Hora && item.IdAtencion == turno.IdAtencion)
                        {
                            ViewBag.advertencia = "Ya hay alguien asignado en este horario";
                            valor = 1;
                        }
                    }
                    if (valor == 1)
                    {

                    }
                    else
                    {
                        db.Turnos.Add(turno);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
            }

           

            ViewBag.IdAtencion = new SelectList(lstatencion, "Id", "Id", turno.IdAtencion);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
            return View(turno);
        }

        // GET: Turno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAtencion = new SelectList(db.Atenciones, "Id", "Id", turno.IdAtencion);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoEspecialidad,Fecha,IdPaciente,IdAtencion,Hora,Abonado")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAtencion = new SelectList(db.Atenciones, "Id", "Id", turno.IdAtencion);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
            return View(turno);
        }

        // GET: Turno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turno turno = db.Turnos.Find(id);
            db.Turnos.Remove(turno);
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
