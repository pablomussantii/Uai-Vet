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
        public int IdTurno { get; set; }

        //public Cliente Cliente { get; set; }

        public Turno Turno { get; set; }
    }


    [MetadataType(typeof(FacturaServicioMetadata))]

    public partial class FacturaServicio 
    {
        public class FacturaServicioMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }
            //[ForeignKey("Cliente")]
            //[Column(Order = 2)]
            //[Required]
            //public int IdCliente { get; set; }
            [ForeignKey("Turno")]
            [Column(Order = 3)]
            [Required]
            public int IdTurno { get; set; }
            [Required]
            public DateTime Fecha { get; set; }
            [Required]
            public double Monto { get; set; }

        }
    }


}
