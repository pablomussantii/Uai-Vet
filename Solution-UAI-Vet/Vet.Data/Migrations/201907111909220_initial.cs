namespace Vet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atencions",
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
                .ForeignKey("dbo.Doctors", t => t.IdDoctor, cascadeDelete: true)
                .ForeignKey("dbo.Salas", t => t.IdSala, cascadeDelete: true)
                .Index(t => t.IdDoctor)
                .Index(t => t.IdSala);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        TipoEspecialidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Localizacion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Genero = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.DetalleFacturaProductoes",
                c => new
                    {
                        IdFacturaProducto = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdFacturaProducto, t.IdProducto })
                .ForeignKey("dbo.FacturaProductoes", t => t.IdFacturaProducto, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdFacturaProducto)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.FacturaProductoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.IdCliente, cascadeDelete: true)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Precio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacturaServicios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTurno = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Turnoes", t => t.IdTurno, cascadeDelete: true)
                .Index(t => t.IdTurno);
            
            CreateTable(
                "dbo.Turnoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPaciente = c.Int(nullable: false),
                        IdAtencion = c.Int(nullable: false),
                        TipoEspecialidad = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Hora = c.Int(nullable: false),
                        Abonado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Atencions", t => t.IdAtencion, cascadeDelete: true)
                .ForeignKey("dbo.Pacientes", t => t.IdPaciente, cascadeDelete: true)
                .Index(t => t.IdPaciente)
                .Index(t => t.IdAtencion);
            
            CreateTable(
                "dbo.HistorialPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPaciente = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pacientes", t => t.IdPaciente, cascadeDelete: true)
                .Index(t => t.IdPaciente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistorialPacientes", "IdPaciente", "dbo.Pacientes");
            DropForeignKey("dbo.FacturaServicios", "IdTurno", "dbo.Turnoes");
            DropForeignKey("dbo.Turnoes", "IdPaciente", "dbo.Pacientes");
            DropForeignKey("dbo.Turnoes", "IdAtencion", "dbo.Atencions");
            DropForeignKey("dbo.DetalleFacturaProductoes", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.DetalleFacturaProductoes", "IdFacturaProducto", "dbo.FacturaProductoes");
            DropForeignKey("dbo.FacturaProductoes", "IdCliente", "dbo.Clientes");
            DropForeignKey("dbo.Pacientes", "ClientId", "dbo.Clientes");
            DropForeignKey("dbo.Atencions", "IdSala", "dbo.Salas");
            DropForeignKey("dbo.Atencions", "IdDoctor", "dbo.Doctors");
            DropIndex("dbo.HistorialPacientes", new[] { "IdPaciente" });
            DropIndex("dbo.Turnoes", new[] { "IdAtencion" });
            DropIndex("dbo.Turnoes", new[] { "IdPaciente" });
            DropIndex("dbo.FacturaServicios", new[] { "IdTurno" });
            DropIndex("dbo.FacturaProductoes", new[] { "IdCliente" });
            DropIndex("dbo.DetalleFacturaProductoes", new[] { "IdProducto" });
            DropIndex("dbo.DetalleFacturaProductoes", new[] { "IdFacturaProducto" });
            DropIndex("dbo.Pacientes", new[] { "ClientId" });
            DropIndex("dbo.Atencions", new[] { "IdSala" });
            DropIndex("dbo.Atencions", new[] { "IdDoctor" });
            DropTable("dbo.HistorialPacientes");
            DropTable("dbo.Turnoes");
            DropTable("dbo.FacturaServicios");
            DropTable("dbo.Productoes");
            DropTable("dbo.FacturaProductoes");
            DropTable("dbo.DetalleFacturaProductoes");
            DropTable("dbo.Pacientes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Salas");
            DropTable("dbo.Doctors");
            DropTable("dbo.Atencions");
        }
    }
}
