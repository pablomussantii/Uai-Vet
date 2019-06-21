using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public class Patient : IEntity
    {
        public Patient()
        {

        }
        public int Id { get; set; }
        public Client Owner { get; private set; }
        public int ClientId { get; set; }
        public Gender Gender { get; set; }
    }
}
