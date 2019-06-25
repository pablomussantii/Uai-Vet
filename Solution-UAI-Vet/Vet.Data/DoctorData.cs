using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain;

namespace Vet.Data
{
    public class RepositoryDoctor : IRepository<Doctor>
    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            Doctor doctor = db.Doctores.Find(id);
            db.Doctores.Remove(doctor);
            db.SaveChanges();
        }

        public Doctor GetById(int id)
        {
            var db = new VetDbContext();
            return db.Doctores.Find(id);
        }

        public void Insert(Doctor doctor)
        {
            var db = new VetDbContext();
            db.Doctores.Add(doctor);
            db.SaveChanges();
        }

        public IEnumerable<Doctor> List()
        {
            var context = new VetDbContext();
            var doctores = context.Doctores.ToList();
            return doctores;

        }

        public void Update(Doctor entity)
        {
            var context = new VetDbContext();
            Doctor editdoctor = context.Doctores.Find(entity.Id);
            if (entity != null)
            {
                editdoctor.Nombre = entity.Nombre;
                editdoctor.Email = entity.Email;

            }
            context.SaveChanges();
        }
    }
}
