using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vet.Domain;
using Vet.Services;
using Vet.Data;

namespace Vet.Webside.Controllers
{
    public class PacienteController : Controller
    {
   
        public ActionResult Index()
        {
            var pacientes = new RepositoryPatient().List();
            return View(pacientes);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Paciente model)
        {
            new RepositoryPatient().Insert(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            new RepositoryPatient().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Paciente paciente = new RepositoryPatient().GetById(id);
            return View(paciente);
        }
        [HttpPost]
        public ActionResult Update(Paciente paciente)
        {
            new RepositoryPatient().Update(paciente);
            return RedirectToAction("Index");
        }
    }
}