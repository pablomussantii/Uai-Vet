using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain.SharedKernel;

namespace Vet.Domain
{
    public class Paciente : IEntity
    {
        public Paciente()
        {

        }
        public int Id { get; set; }
        public Cliente Dueño { get; private set; }
        public int ClientId { get; set; }
        public Genero Genero { get; set; }
        public string Nombre { get; set; }
    }
}
