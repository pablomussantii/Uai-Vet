using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Domain;
using Vet.Services;

namespace Vet.Data
{
    public class RepositoryFacturaProducto : IRepository<FacturaProducto>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            FacturaProducto Objeto = db.FacturaProductos.Find(id);
            db.FacturaProductos.Remove(Objeto);
            db.SaveChanges();
        }

        public FacturaProducto GetById(int id)
        {
            var db = new VetDbContext();
            return db.FacturaProductos.Find(id);
        }

        public void Insert(FacturaProducto Objeto)
        {
            var db = new VetDbContext();
            db.FacturaProductos.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<FacturaProducto> List()
        {
            var context = new VetDbContext();
            var Objetos = context.FacturaProductos.ToList();
            return Objetos;

        }

        public void Update(FacturaProducto entity)
        {
            var context = new VetDbContext();
            FacturaProducto edit = context.FacturaProductos.Find(entity.Id);
            if (entity != null)
            {
                edit.Fecha = entity.Fecha;
                edit.IdCliente = entity.IdCliente;
                edit.Monto = entity.Monto;

            }
            context.SaveChanges();
        }
    }
}
