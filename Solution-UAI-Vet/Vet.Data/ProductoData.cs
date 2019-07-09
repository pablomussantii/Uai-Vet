using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Domain;
using Vet.Services;

namespace Vet.Data
{
    public class RepositoryProducto : IRepository<Producto>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            Producto Objeto = db.Productos.Find(id);
            db.Productos.Remove(Objeto);
            db.SaveChanges();
        }

        public Producto GetById(int id)
        {
            var db = new VetDbContext();
            return db.Productos.Find(id);
        }

        public void Insert(Producto Objeto)
        {
            var db = new VetDbContext();
            db.Productos.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<Producto> List()
        {
            var context = new VetDbContext();
            var Objetos = context.Productos.ToList();
            return Objetos;

        }

        public void Update(Producto entity)
        {
            var context = new VetDbContext();
            Producto edit = context.Productos.Find(entity.Id);
            if (entity != null)
            {
                edit.Nombre = entity.Nombre;
                edit.Precio = entity.Precio;

            }
            context.SaveChanges();
        }
    }
}
