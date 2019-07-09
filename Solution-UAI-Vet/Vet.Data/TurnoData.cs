using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Domain;
using Vet.Services;

namespace Vet.Data
{

    public class RepositoryTurno : IRepository<Turno>

    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            Turno Objeto = db.Turnos.Find(id);
            db.Turnos.Remove(Objeto);
            db.SaveChanges();
        }

        public Turno GetById(int id)
        {
            var db = new VetDbContext();
            return db.Turnos.Find(id);
        }

        public void Insert(Turno Objeto)
        {
            var db = new VetDbContext();
            db.Turnos.Add(Objeto);
            db.SaveChanges();
        }

        public IEnumerable<Turno> List()
        {
            var context = new VetDbContext();
            var Objetos = context.Turnos.ToList();
            return Objetos;

        }

        public void Update(Turno entity)
        {
            var context = new VetDbContext();
            Turno edit = context.Turnos.Find(entity.Id);
            if (entity != null)
            {
                edit.Abonado = entity.Abonado;
                edit.Fecha = entity.Fecha;
                edit.TipoEspecialidad = entity.TipoEspecialidad;
                edit.Hora = entity.Hora;
                edit.IdPaciente = entity.IdPaciente;
                edit.IdSala = entity.IdSala;

            }
            context.SaveChanges();
        }
    }


}
