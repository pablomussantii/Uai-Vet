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
    public class DoctorController : Controller
    {
        public ActionResult Index()
        {
            var doctores = new RepositoryDoctor().List();
            return View(doctores);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Doctor model)
        {
            new RepositoryDoctor().Insert(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            new RepositoryDoctor().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Doctor doctor = new RepositoryDoctor().GetById(id);
            return View(doctor);
        }
        [HttpPost]
        public ActionResult Update(Doctor doctor)
        {
            new RepositoryDoctor().Update(doctor);
            return RedirectToAction("Index");
        }
    }
}