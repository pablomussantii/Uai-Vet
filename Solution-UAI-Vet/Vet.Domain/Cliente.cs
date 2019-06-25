using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public class Cliente : IEntity
    {
        public Cliente()
        {
            Pacientes = new List<Paciente>();
        }

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public IList<Paciente> Pacientes { get; private set; }
    }
}
