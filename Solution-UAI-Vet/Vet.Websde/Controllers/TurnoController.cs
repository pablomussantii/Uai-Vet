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
            RepositoryAtencion repositoryatencion = new RepositoryAtencion();

            var turnos = db.Turnos.Include(t => t.Atencion).Include(t => t.Paciente);

            List<Turno> tur = new List<Turno>();
            tur = turnos.ToList();

            IEnumerable<Turno> lstordenada = tur.OrderBy(User => User.Fecha);

            foreach (var item in lstordenada)
            {
                foreach (var item2 in repositoryatencion.List())
                {
                    if (item.IdAtencion == item2.Id)
                    {
                        item.Atencion = item2;

                        foreach (var item3 in repositoryDoctor.List())
                        {
                            if (item.Atencion.IdDoctor == item3.Id)
                            {
                                item.Atencion.Doctor = item3;
                            }
                        }
                    }
                }
           
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
            RepositoryDoctor repositoryDoctor = new RepositoryDoctor();

            int valoratencion = 1;
            string diaturno;
            diaturno = turno.Fecha.DayOfWeek.ToString();
            List<Atencion> lstatencionvalidas = new List<Atencion>();
            if (turno.IdAtencion == 0)
            {
                foreach (var item in repositoryatencion.List())
                {
                    turno.IdAtencion = item.Id;
                    valoratencion = turno.verificarcoordinacionconatencion(repositoryatencion, turno);

                    if (valoratencion == 0)
                    {

                        if (turno.TipoEspecialidad == item.TipoEspecialidad)
                        {

                            if (turno.Atencion.Dia.ToString() == diaturno)
                            {
                                foreach (var item3 in repositoryDoctor.List())
                                {
                                    if (item.IdDoctor == item3.Id)
                                    {
                                        item.Doctor = item3;
                                    }
                                }

                                lstatencionvalidas.Add(item);
                            }

                            valoratencion = 1;


                        }

                    }

                }
            
                turno.IdAtencion = 0;
                //IEnumerable<Atencion> lst;
                //lst = lstatencionvalidas;
                if (lstatencionvalidas.Count > 0)
                {
                    //ViewBag.Atenciones = lstatencionvalidas.Select(m => new SelectListItem { Text = m.Id.ToString(), Value = m.Id.ToString() });
                    ViewBag.IdAtencion = new SelectList(lstatencionvalidas, "Id", "Doctor.Nombre");
                }

                ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", turno.IdPaciente);
                return View(turno);

             }

                //List<Atencion> lstatencion = new List<Atencion>();

                //foreach (var item in repositoryatencion.List())
                //{
                //    if (item.TipoEspecialidad == turno.TipoEspecialidad)
                //    {
                //        lstatencion.Add(item);
                //    }
                //}
                //ViewBag.IdAtencion = new SelectList(lstatencion, "Id", "Id", turno.IdAtencion);
                if (ModelState.IsValid)
                {

                    int valor2 = 0;

                    valor2 = turno.validarhora(turno);






                    Atencion natencion = new Atencion();
                    natencion = repositoryatencion.GetById(turno.IdAtencion);
                    int valor = 0;


                valor = turno.verificarrepeticion(repositoryTurno, turno);



                int valor3 = 1;

                //valor3 = turno.verificarcoordinacionconatencion(repositoryatencion, turno);


                valor3 = 0;

                    if (valor3 == 1)
                    {
                        ViewBag.advertencia4 = string.Format("El doctor no se encuentra en este horario, esta en el horario de {0} .", natencion.HorarioTurno);
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
                        //int valor99 = 0;
                        //foreach (var item in repositoryatencion.List())
                        //{

                        //    if (valor99 < item.Id)
                        //    {
                        //        valor99 = item.Id;
                        //    }

                        //}
                        //repositoryatencion.Delete(valor99);
                        //valor99 = 0;
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
            turno.Cancelado = true;
            db.Entry(turno).State = EntityState.Modified;
            //db.Turnos.Remove(turno);
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
            ViewBag.Diaactual = DateTime.Now;
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
                        if (item.Cancelado == true)
                        {

                        }
                        else
                        {
                            lstturno.Add(item);
                        }

                    }
                }

                IEnumerable<Turno> lstordenada = lstturno.OrderBy(Turno => Turno.Hora);

                return View(lstordenada);
            


        }

        public JsonResult GetEvents()
        {
            using (VetDbContext dc = new VetDbContext())
            {
                var events = dc.Turnos.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}
