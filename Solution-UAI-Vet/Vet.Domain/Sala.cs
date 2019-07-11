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
    public partial class Sala : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Localizacion { get; set; }

        //public SharedKernel.TipoEspecialidad Especialidad { get; set; }
    }
    [MetadataType(typeof(SalaMetadata))]
    public partial class Sala
    {
        public class SalaMetadata
        {
            [Key]
            public int Id { get; set; }
            [StringLength(50)]
            [Required]
            public string Nombre { get; set; }
            [StringLength(50)]
            [Required]
            public string Localizacion { get; set; }
            //[Required]
            //public SharedKernel.TipoEspecialidad Especialidad { get; set; }
        }
    }
}
