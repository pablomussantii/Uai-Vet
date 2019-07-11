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
    public abstract partial class Factura : IEntity
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        
        public double Monto { get; set; }

      

    }

    [MetadataType(typeof(FacturaMetadata))]

    public abstract partial class Factura
    {
        public class FacturaMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            [Required]
            public DateTime Fecha { get; set; }
            [Required]
            public double Monto { get; set; }
        }
    }

}
