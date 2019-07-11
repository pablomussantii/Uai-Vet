using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet.Domain
{
    public partial class FacturaServicio : Factura
    {
        public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }

        public Cliente Cliente { get; set; }
    }


    [MetadataType(typeof(FacturaServicioMetadata))]

    public partial class FacturaServicio : Factura
    {
        public class FacturaServicioMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            [ForeignKey("Cliente")]
            [Column(Order = 2)]
            [Required]
            public int IdCliente { get; set; }
            [Required]
            public DateTime Fecha { get; set; }
            [Required]
            public double Monto { get; set; }
            [Required]
            public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
        }
    }


}
