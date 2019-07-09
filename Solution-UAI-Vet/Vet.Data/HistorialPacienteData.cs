using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Domain;
using Vet.Services;

namespace Vet.Data
{
    public class RepositoryHistorialPaciente : IRepository<HistorialPaciente>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            HistorialPaciente Objeto = db.HistorialPacientes.Find(id);
            db.HistorialPacientes.Remove(Objeto);
            db.SaveChanges();
        }

        public HistorialPaciente GetById(int id)
        {
            var db = new VetDbContext();
            return db.HistorialPacientes.Find(id);
        }

        public void Insert(HistorialPaciente Objeto)
        {
            var db = new VetDbContext();
            db.HistorialPacientes.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<HistorialPaciente> List()
        {
            var context = new VetDbContext();
            var Objetos = context.HistorialPacientes.ToList();
            return Objetos;

        }

        public void Update(HistorialPaciente entity)
        {
            var context = new VetDbContext();
            HistorialPaciente edit = context.HistorialPacientes.Find(entity.Id);
            if (entity != null)
            {
                edit.IdPaciente = entity.IdPaciente;
                edit.Descripcion = entity.Descripcion;

            }
            context.SaveChanges();
        }
    }
}
