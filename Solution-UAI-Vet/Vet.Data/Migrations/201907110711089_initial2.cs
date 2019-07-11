namespace Vet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistorialPacientes", "Fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistorialPacientes", "Fecha");
        }
    }
}
