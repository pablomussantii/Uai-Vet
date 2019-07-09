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
   public partial class Producto : IEntity
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }
    }

    [MetadataType(typeof(ProductoMetadata))]

    public partial class Producto
    {
        public class ProductoMetadata
        {
            [Key]
            public int Id { get; set; }
            [StringLength(50)]
            [Required]
            public string Nombre { get; set; }
            [Required]
            public double Precio { get; set; }

        }
    }
}
