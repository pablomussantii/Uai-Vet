using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using Vet.Domain;


namespace Vet.Data
{
    public class VetDbContext : DbContext
    {
        public VetDbContext() : base("Data Source=DESKTOP-0LE6IUG;Initial Catalog=bd4;Integrated Security=True")
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Atencion> Atenciones { get; set; }
        public DbSet<FacturaProducto> FacturaProductos { get; set; }
        public DbSet<FacturaServicio> FacturaServicios { get; set; }
        public DbSet<HistorialPaciente> HistorialPacientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        //public DbSet<DetalleFacturaProducto> DetalleFacturaProductos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Sala> Salas { get; set; }


    }

    
}
