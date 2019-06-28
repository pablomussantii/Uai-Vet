using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Vet.Domain
{
    public partial class Paciente : IEntity
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

    [MetadataType(typeof(PacienteMetadata))]
    public partial class Cliente
    {
        public class PacienteMetadata
        {
            [Key]
            public int Id { get; set; }
            [StringLength(50)]
            [Required]
            public string Nombre { get; set; }
            [Required]
            public Genero Genero { get; set; }
        }
    }
}
