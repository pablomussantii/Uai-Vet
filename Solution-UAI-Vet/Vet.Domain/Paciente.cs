using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vet.Domain
{
    public partial class Paciente : IEntity
    {
        public Paciente()
        {

        }
        public int Id { get; set; }
        public Cliente Cliente { get; private set; }
        public int ClientId { get; set; }
        public Genero Genero { get; set; }
        public string Nombre { get; set; }


        public byte[] ImagenMascota { get; set; }

        public string Raza { get; set; }

        public string TipodeSangre { get; set; }

    }

    [MetadataType(typeof(PacienteMetadata))]
    public partial class Paciente
    {
        public class PacienteMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            [ForeignKey("Cliente")]
            [Column(Order = 2)]
            [Required]
            public int ClientId { get; set; }
            [StringLength(50)]
            [Required]
            public string Nombre { get; set; }
            [Required]
            public Genero Genero { get; set; }
            
            public byte[] ImagenMascota { get; set; }
            [Required]
            public string Raza { get; set; }
            [Required]
            public string TipodeSangre { get; set; }
        }
    }
}
