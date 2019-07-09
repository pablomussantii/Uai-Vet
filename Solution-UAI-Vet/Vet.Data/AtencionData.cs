using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Domain;
using Vet.Services;

namespace Vet.Data
{
    public class RepositoryAtencion : IRepository<Atencion>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            Atencion Objeto = db.Atenciones.Find(id);
            db.Atenciones.Remove(Objeto);
            db.SaveChanges();
        }

        public Atencion GetById(int id)
        {
            var db = new VetDbContext();
            return db.Atenciones.Find(id);
        }

        public void Insert(Atencion Objeto)
        {
            var db = new VetDbContext();
            db.Atenciones.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<Atencion> List()
        {
            var context = new VetDbContext();
            var Objetos = context.Atenciones.ToList();
            return Objetos;

        }

        public void Update(Atencion entity)
        {
            var context = new VetDbContext();
            Atencion edit = context.Atenciones.Find(entity.Id);
            if (entity != null)
            {
                edit.IdDoctor = entity.IdDoctor;
                edit.IdSala = entity.IdSala;
                edit.TipoEspecialidad = entity.TipoEspecialidad;

            }
            context.SaveChanges();
        }
    }
}
