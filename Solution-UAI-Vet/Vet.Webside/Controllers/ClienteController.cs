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
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            var Clientes = new RepositoryClient().List();
            return View(Clientes);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Cliente model)
        {
            new RepositoryClient().Insert(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            new RepositoryClient().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Cliente cliente = new RepositoryClient().GetById(id);
            return View(cliente);
        }
        [HttpPost]
        public ActionResult Update(Cliente cliente)
        {
            new RepositoryClient().Update(cliente);
            return RedirectToAction("Index");
        }

    }
}