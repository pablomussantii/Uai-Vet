namespace Vet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pacientes", "Dueño_Id", "dbo.Clientes");
            DropIndex("dbo.Pacientes", new[] { "Dueño_Id" });
            DropColumn("dbo.Pacientes", "ClientId");
            RenameColumn(table: "dbo.Pacientes", name: "Dueño_Id", newName: "ClientId");
            AddColumn("dbo.FacturaProductoes", "Cliente_Id", c => c.Int());
            AddColumn("dbo.FacturaServicios", "Cliente_Id", c => c.Int());
            AlterColumn("dbo.Pacientes", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pacientes", "ClientId");
            CreateIndex("dbo.FacturaProductoes", "Cliente_Id");
            CreateIndex("dbo.FacturaServicios", "Cliente_Id");
            AddForeignKey("dbo.FacturaProductoes", "Cliente_Id", "dbo.Clientes", "Id");
            AddForeignKey("dbo.FacturaServicios", "Cliente_Id", "dbo.Clientes", "Id");
            AddForeignKey("dbo.Pacientes", "ClientId", "dbo.Clientes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pacientes", "ClientId", "dbo.Clientes");
            DropForeignKey("dbo.FacturaServicios", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.FacturaProductoes", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.FacturaServicios", new[] { "Cliente_Id" });
            DropIndex("dbo.FacturaProductoes", new[] { "Cliente_Id" });
            DropIndex("dbo.Pacientes", new[] { "ClientId" });
            AlterColumn("dbo.Pacientes", "ClientId", c => c.Int());
            DropColumn("dbo.FacturaServicios", "Cliente_Id");
            DropColumn("dbo.FacturaProductoes", "Cliente_Id");
            RenameColumn(table: "dbo.Pacientes", name: "ClientId", newName: "Dueño_Id");
            AddColumn("dbo.Pacientes", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pacientes", "Dueño_Id");
            AddForeignKey("dbo.Pacientes", "Dueño_Id", "dbo.Clientes", "Id");
        }
    }
}
