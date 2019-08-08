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
    public class AtencionController : Controller
    {
        private VetDbContext db = new VetDbContext();

        log4net.ILog log = log4net.LogManager.GetLogger(typeof(AtencionController));

        // GET: Atencion
        public ActionResult Index()
        {
            var atenciones = db.Atenciones.Include(a => a.Doctor).Include(a => a.Sala);
            return View(atenciones.ToList());
        }

        // GET: Atencion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atencion atencion = db.Atenciones.Find(id);
            if (atencion == null)
            {
                return HttpNotFound();
            }
            return View(atencion);
        }

        // GET: Atencion/Create
        public ActionResult Create()
        {
            ViewBag.IdDoctor = new SelectList(db.Doctores, "Id", "Nombre");
            ViewBag.IdSala = new SelectList(db.Salas, "Id", "Nombre");
            return View();
        }

        // POST: Atencion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdDoctor,IdSala,TipoEspecialidad,HorarioTurno,Dia")] Atencion atencion)
        {
            if (ModelState.IsValid)
            {

                RepositoryDoctor repositoryDoctor = new RepositoryDoctor();

                foreach (var item in repositoryDoctor.List())
                {
                    if (item.Id == atencion.IdDoctor)
                    {
                        atencion.TipoEspecialidad = item.TipoEspecialidad;
                    }
                }


                int valor = 0;
                RepositoryAtencion repository = new RepositoryAtencion();
             
                valor = atencion.verificardisponibilidadsala(repository, atencion);
                //foreach (var x in repository.List())
                //{
                //    if (x.Dia == atencion.Dia && x.HorarioTurno == atencion.HorarioTurno && x.IdSala == atencion.IdSala)
                //    {
                //        valor = 1;
                //    }
                //}

                if (valor==1)
                {
                    string aviso = "Ya se encuentra un Empleado en ese dia, ese turno y esa sala.";
                   ViewBag.advertencia = aviso;
                }
                else
                {
                    db.Atenciones.Add(atencion);
                    db.SaveChanges();
                    log.Info("Creacion de atencion");
                    return RedirectToAction("Index");
                }
            }

            ViewBag.IdDoctor = new SelectList(db.Doctores, "Id", "Nombre", atencion.IdDoctor);
            ViewBag.IdSala = new SelectList(db.Salas, "Id", "Nombre", atencion.IdSala);
            return View(atencion);
        }

        // GET: Atencion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atencion atencion = db.Atenciones.Find(id);
            if (atencion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDoctor = new SelectList(db.Doctores, "Id", "Nombre", atencion.IdDoctor);
            ViewBag.IdSala = new SelectList(db.Salas, "Id", "Nombre", atencion.IdSala);
            return View(atencion);
        }

        // POST: Atencion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdDoctor,IdSala,TipoEspecialidad,HorarioTurno,Dia")] Atencion atencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atencion).State = EntityState.Modified;
                db.SaveChanges();
                log.Info("Edicion de atencion");
                return RedirectToAction("Index");
            }
            ViewBag.IdDoctor = new SelectList(db.Doctores, "Id", "Nombre", atencion.IdDoctor);
            ViewBag.IdSala = new SelectList(db.Salas, "Id", "Nombre", atencion.IdSala);
            return View(atencion);
        }

        // GET: Atencion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atencion atencion = db.Atenciones.Find(id);
            if (atencion == null)
            {
                return HttpNotFound();
            }
            return View(atencion);
        }

        // POST: Atencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atencion atencion = db.Atenciones.Find(id);
            db.Atenciones.Remove(atencion);
            db.SaveChanges();
            log.Info("Eliminacion de atencion");
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
