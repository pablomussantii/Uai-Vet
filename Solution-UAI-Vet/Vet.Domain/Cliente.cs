using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public partial class Cliente : IEntity
    {
        public Cliente()
        {
            Pacientes = new List<Paciente>();
        }

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public IList<Paciente> Pacientes { get; private set; }
    }

    [MetadataType(typeof(ClienteMetadata))]

     
    public partial class Cliente
    {
        public class ClienteMetadata
        {
            [Key]
            public int Id { get; set; }
            [StringLength(50)]
            [Required]
            public string NombreCompleto { get; set; }
            [StringLength(50)]
            [Required]
            public string Email { get; set; }
            [Required]
            public int Dni { get; set; }
            [Required]
            public int Telefono { get; set; }
            [Required]
            public string Direccion { get; set; }

        }
    }

}
