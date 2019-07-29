namespace Vet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atencion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdDoctor = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        TipoEspecialidad = c.Int(nullable: false),
                        HorarioTurno = c.Int(nullable: false),
                        Dia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctor", t => t.IdDoctor, cascadeDelete: true)
                .ForeignKey("dbo.Sala", t => t.IdSala, cascadeDelete: true)
                .Index(t => t.IdDoctor)
                .Index(t => t.IdSala);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        TipoEspecialidad = c.Int(nullable: false),
                        Telefono = c.Int(nullable: false),
                        Direccion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sala",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Localizacion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Dni = c.Int(nullable: false),
                        Telefono = c.Int(nullable: false),
                        Direccion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Paciente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Genero = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        ImagenMascota = c.Binary(),
                        Raza = c.String(nullable: false),
                        TipodeSangre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.FacturaProducto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdCliente)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Precio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacturaServicio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTurno = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Turno", t => t.IdTurno, cascadeDelete: true)
                .Index(t => t.IdTurno);
            
            CreateTable(
                "dbo.Turno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPaciente = c.Int(nullable: false),
                        IdAtencion = c.Int(nullable: false),
                        TipoEspecialidad = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Hora = c.Int(nullable: false),
                        Abonado = c.Boolean(nullable: false),
                        Cancelado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Atencion", t => t.IdAtencion, cascadeDelete: true)
                .ForeignKey("dbo.Paciente", t => t.IdPaciente, cascadeDelete: true)
                .Index(t => t.IdPaciente)
                .Index(t => t.IdAtencion);
            
            CreateTable(
                "dbo.HistorialPaciente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPaciente = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Paciente", t => t.IdPaciente, cascadeDelete: true)
                .Index(t => t.IdPaciente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistorialPaciente", "IdPaciente", "dbo.Paciente");
            DropForeignKey("dbo.FacturaServicio", "IdTurno", "dbo.Turno");
            DropForeignKey("dbo.Turno", "IdPaciente", "dbo.Paciente");
            DropForeignKey("dbo.Turno", "IdAtencion", "dbo.Atencion");
            DropForeignKey("dbo.FacturaProducto", "IdProducto", "dbo.Producto");
            DropForeignKey("dbo.FacturaProducto", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.Paciente", "ClientId", "dbo.Cliente");
            DropForeignKey("dbo.Atencion", "IdSala", "dbo.Sala");
            DropForeignKey("dbo.Atencion", "IdDoctor", "dbo.Doctor");
            DropIndex("dbo.HistorialPaciente", new[] { "IdPaciente" });
            DropIndex("dbo.Turno", new[] { "IdAtencion" });
            DropIndex("dbo.Turno", new[] { "IdPaciente" });
            DropIndex("dbo.FacturaServicio", new[] { "IdTurno" });
            DropIndex("dbo.FacturaProducto", new[] { "IdProducto" });
            DropIndex("dbo.FacturaProducto", new[] { "IdCliente" });
            DropIndex("dbo.Paciente", new[] { "ClientId" });
            DropIndex("dbo.Atencion", new[] { "IdSala" });
            DropIndex("dbo.Atencion", new[] { "IdDoctor" });
            DropTable("dbo.HistorialPaciente");
            DropTable("dbo.Turno");
            DropTable("dbo.FacturaServicio");
            DropTable("dbo.Producto");
            DropTable("dbo.FacturaProducto");
            DropTable("dbo.Paciente");
            DropTable("dbo.Cliente");
            DropTable("dbo.Sala");
            DropTable("dbo.Doctor");
            DropTable("dbo.Atencion");
        }
    }
}
