using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vet.Domain;
using Vet.Services;

namespace Vet.Webside.Controllers
{
    public class SalaController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            var rooms = new RepositoryRoom().List();
            return View(rooms);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Room model)
        {
            new RepositoryRoom().Insert(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            new RepositoryRoom().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Room room = new RepositoryRoom().GetById(id);
            return View(room);
        }
        [HttpPost]
        public ActionResult Update(Room room)
        {
            new RepositoryRoom().Update(room);
            return RedirectToAction("Index");
        }

    }
}