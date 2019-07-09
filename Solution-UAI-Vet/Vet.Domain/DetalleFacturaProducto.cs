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
   public partial class DetalleFacturaProducto  
    {
        public int IdFacturaProducto { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public Producto Producto { get; set; }
        public FacturaProducto FacturaProducto { get; set; }


    }



    [MetadataType(typeof(DetalleFacturaProductoMetadata))]

    public partial class DetalleFacturaProducto
    {
        public class DetalleFacturaProductoMetadata
        {
            [Key]
            [ForeignKey("FacturaProducto")]
            [Column(Order = 1)]
            [Required]
            public int IdFacturaProducto { get; set; }
            [Key]
            [ForeignKey("Producto")]
            [Column(Order = 2)]
            [Required]
            public int IdProducto { get; set; }
            [Required]
           public int Cantidad { get; set; }

        }
    }
}
