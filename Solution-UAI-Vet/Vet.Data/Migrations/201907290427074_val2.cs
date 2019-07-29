namespace Vet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class val2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Atencions", newName: "Atencion");
            RenameTable(name: "dbo.Doctors", newName: "Doctor");
            RenameTable(name: "dbo.Salas", newName: "Sala");
            RenameTable(name: "dbo.Clientes", newName: "Cliente");
            RenameTable(name: "dbo.Pacientes", newName: "Paciente");
            RenameTable(name: "dbo.FacturaProductoes", newName: "FacturaProducto");
            RenameTable(name: "dbo.Productoes", newName: "Producto");
            RenameTable(name: "dbo.FacturaServicios", newName: "FacturaServicio");
            RenameTable(name: "dbo.Turnoes", newName: "Turno");
            RenameTable(name: "dbo.HistorialPacientes", newName: "HistorialPaciente");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.HistorialPaciente", newName: "HistorialPacientes");
            RenameTable(name: "dbo.Turno", newName: "Turnoes");
            RenameTable(name: "dbo.FacturaServicio", newName: "FacturaServicios");
            RenameTable(name: "dbo.Producto", newName: "Productoes");
            RenameTable(name: "dbo.FacturaProducto", newName: "FacturaProductoes");
            RenameTable(name: "dbo.Paciente", newName: "Pacientes");
            RenameTable(name: "dbo.Cliente", newName: "Clientes");
            RenameTable(name: "dbo.Sala", newName: "Salas");
            RenameTable(name: "dbo.Doctor", newName: "Doctors");
            RenameTable(name: "dbo.Atencion", newName: "Atencions");
        }
    }
}
