using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet.Data
{
    public class RepositoryRoom
    {


        public void Delete(int id)
        {
            var db = new VetDbContext();
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
        }

        public Room GetById(int id)
        {
            var db = new VetDbContext();
            return db.Rooms.Find(id);
        }

        public void Insert(Room room)
        {
            var db = new VetDbContext();
            db.Rooms.Add(room);
            db.SaveChanges();
        }

        public IEnumerable<Room> List()
        {
            var context = new VetDbContext();
            var rooms = context.Rooms.ToList();
            return rooms;

        }

        public void Update(Room entity)
        {
            var context = new VetDbContext();
            Room editRoom = context.Rooms.Find(entity.Id);
            if (entity != null)
            {
                editRoom.Location = entity.Location;
                editRoom.Name = entity.Name;
            }
            context.SaveChanges();
        }
    }
}
