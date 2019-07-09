using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public partial class HistorialPaciente : IEntity
    {
        public int Id { get; set; }

        public int IdPaciente { get; set; }

        public string Descripcion { get; set; }

        public Paciente Paciente { get; set; }

    }

    [MetadataType(typeof(HistorialPacienteMetadata))]

    public partial class HistorialPaciente
    {
        public class HistorialPacienteMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            [ForeignKey("Paciente")]
            [Column(Order = 2)]
            [Required]
            public int IdPaciente { get; set; }
            [Required]
            public string Descripcion { get; set; }

        }
    }
}
