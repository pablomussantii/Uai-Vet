using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain;

namespace Vet.Data
{
    public class RepositoryPatient : IRepository<Paciente>
    {
        public void Delete(int id)
        {
            var db = new VetDbContext();
            Paciente paciente = db.Pacientes.Find(id);
            db.Pacientes.Remove(paciente);
            db.SaveChanges();
        }

        public Paciente GetById(int id)
        {
            var db = new VetDbContext();
            return db.Pacientes.Find(id);
        }

        public void Insert(Paciente paciente)
        {
            var db = new VetDbContext();
            db.Pacientes.Add(paciente);
            db.SaveChanges();
        }

        public IEnumerable<Paciente> List()
        {
            var context = new VetDbContext();
            var pacientes = context.Pacientes.ToList();
            return pacientes;

        }

        public void Update(Paciente entity)
        {
            var context = new VetDbContext();
            Paciente editpaciente = context.Pacientes.Find(entity.Id);
            if (entity != null)
            {
                //editpaciente.Nombre = entity.Nombre;
                //editpaciente.ClientId = entity.ClientId;
                //editpaciente.Genero = entity.Genero;
                //editpaciente.Dueño.Id = entity.Dueño.Id;
                //editpaciente.Dueño.NombreCompleto = entity.Dueño.NombreCompleto;
                //editpaciente.Dueño.Email = entity.Dueño.Email;
                //editpaciente.Dueño.Pacientes.Clear();
                //foreach (var item in entity.Dueño.Pacientes)
                //{
                //    editpaciente.Dueño.Pacientes.Add(item);
                //}
            }
            context.SaveChanges();
        }
    }
}
