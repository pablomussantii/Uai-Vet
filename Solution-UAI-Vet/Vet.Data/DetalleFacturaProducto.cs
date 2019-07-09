using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain;

namespace Vet.Data
{
    public class RepositoryDetalleFacturaProducto : IRepository<DetalleFacturaProducto>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            DetalleFacturaProducto Objeto = db.DetalleFacturaProductos.Find(id);
            db.DetalleFacturaProductos.Remove(Objeto);
            db.SaveChanges();
        }

        public DetalleFacturaProducto GetById(int id)
        {
            var db = new VetDbContext();
            return db.DetalleFacturaProductos.Find(id);
        }

        public void Insert(DetalleFacturaProducto Objeto)
        {
            var db = new VetDbContext();
            db.DetalleFacturaProductos.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<DetalleFacturaProducto> List()
        {
            var context = new VetDbContext();
            var Objetos = context.DetalleFacturaProductos.ToList();
            return Objetos;

        }

        public void Update(DetalleFacturaProducto entity)
        {
            var context = new VetDbContext();
            DetalleFacturaProducto edit = context.DetalleFacturaProductos.Find(entity.IdFacturaProducto,entity.IdProducto);
            if (entity != null)
            {
               
                edit.Cantidad = entity.Cantidad;


            }
            context.SaveChanges();
        }
    }



}
