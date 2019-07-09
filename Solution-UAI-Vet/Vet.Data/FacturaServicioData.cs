using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Domain;
using Vet.Services;

namespace Vet.Data
{
    public class RepositoryFacturaServicio : IRepository<FacturaServicio>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            FacturaServicio Objeto = db.FacturaServicios.Find(id);
            db.FacturaServicios.Remove(Objeto);
            db.SaveChanges();
        }

        public FacturaServicio GetById(int id)
        {
            var db = new VetDbContext();
            return db.FacturaServicios.Find(id);
        }

        public void Insert(FacturaServicio Objeto)
        {
            var db = new VetDbContext();
            db.FacturaServicios.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<FacturaServicio> List()
        {
            var context = new VetDbContext();
            var Objetos = context.FacturaServicios.ToList();
            return Objetos;

        }

        public void Update(FacturaServicio entity)
        {
            var context = new VetDbContext();
            FacturaServicio edit = context.FacturaServicios.Find(entity.Id);
            if (entity != null)
            {
                edit.Fecha = entity.Fecha;
                edit.IdCliente = entity.IdCliente;
                edit.Monto = entity.Monto;
                edit.TipoEspecialidad = entity.TipoEspecialidad;
               

            }
            context.SaveChanges();
        }
    }
}
