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
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(TurnoController));

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
            //ViewBag.IdAtencion = new SelectList(db.Atenciones, "Id", "Id");
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
            RepositoryAtencion repositoryatencion = new RepositoryAtencion();
            RepositoryTurno repositoryTurno = new RepositoryTurno();
            List<Atencion> lstatencion = new List<Atencion>();

            foreach (var item in repositoryatencion.List())
            {
                if (item.TipoEspecialidad == turno.TipoEspecialidad)
                {
                    lstatencion.Add(item);
                }
            }
            ViewBag.IdAtencion = new SelectList(lstatencion, "Id", "Id", turno.IdAtencion);
                if (ModelState.IsValid)
                {

                    int valor2 = 0;

                    valor2 = turno.validarhora(turno);



                    //if (turno.Hora >= 25 || turno.Hora <= -1)
                    //{
                    //    ViewBag.advertencia3 = "La hora ingresada no es valida";
                    //    valor2 = 1;
                    //}
                    //else
                    //{
                    //    valor2 = 0;
                    //}



                    Atencion natencion = new Atencion();
                    natencion = repositoryatencion.GetById(turno.IdAtencion);
                    int valor = 0;
                    //ViewBag.advertencia2 = "No corresponde la atencion con lo que se solicita";
                    //int valor2 = 0;


                    valor = turno.verificarrepeticion(repositoryTurno, turno);
                    
                    //foreach (var item in repositoryTurno.List())
                    //{
                    //    if (item.Fecha == turno.Fecha && item.Hora == turno.Hora && item.IdAtencion == turno.IdAtencion)
                    //    {
                    //        ViewBag.advertencia = "Ya hay alguien asignado en este horario";
                    //        valor = 1;
                    //    }
                    
                    //}

                    int valor3 = 1;

                    valor3 = turno.verificarcoordinacionconatencion(repositoryatencion, turno);

                    //foreach (var item in repositoryatencion.List())
                    //{
                    //    if (item.Id == turno.IdAtencion)
                    //    {
                    //        turno.Atencion = item;
                    //        if (item.HorarioTurno == Domain.SharedKernel.HorarioTurno.Mañana)
                    //        {
                    //            if (turno.Hora >= 6 && turno.Hora <= 12)
                    //            {
                    //                valor3 = 0;
                    //            }
                    //        }

                    //        if (item.HorarioTurno == Domain.SharedKernel.HorarioTurno.Tarde)
                    //        {
                    //            if (turno.Hora >= 13 && turno.Hora <= 19)
                    //            {
                    //                valor3 = 0;
                    //            }

                    //        }

                    //        if (item.HorarioTurno == Domain.SharedKernel.HorarioTurno.Noche)
                    //        {
                    //            if (turno.Hora >= 20 && turno.Hora <= 23)
                    //            {
                    //                valor3 = 0;
                    //            }
                    //            if (turno.Hora >= 0 && turno.Hora <= 5)
                    //            {
                    //                valor3 = 0;
                    //            }

                    //        }
                    //    } 

                    //}

                    if (valor3 == 1)
                    {
                        ViewBag.advertencia4 = string.Format("El doctor no se encuentra en este horario, esta en el horario de {0} .",turno.Atencion.HorarioTurno);
                    }

                    if (valor == 1)
                    {
                        ViewBag.advertencia = "Ya hay alguien asignado en este horario.";
                    }

                    if (valor2 == 1)
                    {
                        ViewBag.advertencia3 = "La hora ingresada no es valida.";
                    }

                    if (valor == 1 || valor2 == 1 || valor3 == 1)
                    {

                    }
                    else
                    {
                        db.Turnos.Add(turno);
                        db.SaveChanges();
                    log.Info("Creacion de turno");
                        return RedirectToAction("Index");
                    }

                
                }

           

           
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
                log.Info("Edicion de turno");
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
            log.Info("Eliminacion de turno");
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

        //[HttpGet]
        //public ActionResult Reserva(DateTime Fecha)
        //{

        //    RepositoryTurno turnos = new RepositoryTurno();
        //    List<Turno> lstturno = new List<Turno>();
        //    foreach (var item in turnos.List())
        //    {
        //        if (item.Fecha.ToString("dd/MM/yy")== ViewBag.Fecha.ToString("dd/MM/yy"))
        //        {
        //            lstturno.Add(item);
        //        }
        //    }
          
        //    IEnumerable<Turno> lstordenada = lstturno.OrderBy(Turno => Turno.Hora);

        //    return View(lstordenada);
        //}

        [HttpGet]

        public ActionResult CReserva()
        {
            var turnos = db.Turnos.Include(t => t.Atencion).Include(t => t.Paciente);
          
            return View(turnos);
        }


        [HttpPost]
        public ActionResult CReserva(DateTime Fecha)
        {

            List<Turno> lstturno = new List<Turno>();
            var turnos = db.Turnos.Include(t => t.Atencion).Include(t => t.Paciente);
            foreach (var item in turnos)
            {
                if (item.Fecha.ToString("dd/MM/yy") == Fecha.ToString("dd/MM/yy"))
                {
                    lstturno.Add(item);
                }
            }

            IEnumerable<Turno> lstordenada = lstturno.OrderBy(Turno => Turno.Hora);

            return View(lstordenada);

        }


    }
}
