using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain;

namespace Vet.Data
{
    public class RepositoryClient : IRepository<Cliente>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            Cliente Cliente = db.Clientes.Find(id);
            db.Clientes.Remove(Cliente);
            db.SaveChanges();
        }

        public Cliente GetById(int id)
        {
            var db = new VetDbContext();
            return db.Clientes.Find(id);
        }

        public void Insert(Cliente cliente)
        {
            var db = new VetDbContext();
            db.Clientes.Add(cliente);
            db.SaveChanges();
        }

        public IEnumerable<Cliente> List()
        {
            var context = new VetDbContext();
            var clientes = context.Clientes.ToList();
            return clientes;

        }

        public void Update(Cliente entity)
        {
            var context = new VetDbContext();
            Cliente editclient = context.Clientes.Find(entity.Id);
            if (entity != null)
            {
                editclient.NombreCompleto = entity.NombreCompleto;
                editclient.Email = entity.Email;
                editclient.Pacientes.Clear();
                foreach (var item in entity.Pacientes)
                {
                    editclient.Pacientes.Add(item);
                }
      
            }
            context.SaveChanges();
        }
    }
}
