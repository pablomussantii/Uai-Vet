using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;
using Vet.Domain.SharedKernel;

namespace Vet.Domain
{
    public partial class Doctor : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
    }

    [MetadataType(typeof(DoctorMetadata))]
    public partial class Doctor
    {
        public class DoctorMetadata
        {
            [Key]
            public int Id { get; set; }
            [StringLength(50)]
            [Required]
            public string Nombre { get; set; }
            [StringLength(50)]
            [Required]
            public string Email { get; set; }
            [Required]
            public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
            [Required]
            public int Telefono { get; set; }
            [Required]
            public string Direccion { get; set; }
        }
    }
}
