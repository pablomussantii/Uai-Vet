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
    public partial class Turno : IEntity
    {
        public int Id { get; set; }
        public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPaciente { get; set; }
        public int IdSala { get; set; }
        public int Hora { get; set; }
        public Boolean Abonado { get; set; }

        public Paciente Paciente { get; set; }
        public Sala Sala { get; set; }
    }

    [MetadataType(typeof(TurnoMetadata))]

    public partial class Turno
    {
        public class TurnoMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            [ForeignKey("Paciente")]
            [Column(Order = 2)]
            [Required]
            public int IdPaciente { get; set; }
            [ForeignKey("Sala")]
            [Column(Order = 3)]
            [Required]
            public int IdSala { get; set; }
            [Required]
            public DateTime Fecha { get; set; }
            [Required]
            public int Hora { get; set; }
            [Required]
            public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
            [Required]
            public Boolean Abonado { get; set; }
        }
    }
}
