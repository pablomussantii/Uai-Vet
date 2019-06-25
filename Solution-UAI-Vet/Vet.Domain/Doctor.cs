using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
