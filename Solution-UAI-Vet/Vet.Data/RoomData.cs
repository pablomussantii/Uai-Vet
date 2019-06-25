using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain;


namespace Vet.Data
{
    public class RepositoryRoom
    {


        public void Delete(int id)
        {
            var db = new VetDbContext();
            Sala room = db.Salas.Find(id);
            db.Salas.Remove(room);
            db.SaveChanges();
        }

        public Sala GetById(int id)
        {
            var db = new VetDbContext();
            return db.Salas.Find(id);
        }

        public void Insert(Sala room)
        {
            var db = new VetDbContext();
            db.Salas.Add(room);
            db.SaveChanges();
        }

        public IEnumerable<Sala> List()
        {
            var context = new VetDbContext();
            var rooms = context.Salas.ToList();
            return rooms;

        }

        public void Update(Sala entity)
        {
            var context = new VetDbContext();
            Sala editRoom = context.Salas.Find(entity.Id);
            if (entity != null)
            {
                editRoom.Localizacion = entity.Localizacion;
                editRoom.Nombre = entity.Nombre;
            }
            context.SaveChanges();
        }
    }
}
