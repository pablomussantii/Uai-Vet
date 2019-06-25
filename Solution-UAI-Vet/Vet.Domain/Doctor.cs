using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet.Services;

namespace Vet.Domain
{
    public partial class Doctor : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
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
        }
    }
}
