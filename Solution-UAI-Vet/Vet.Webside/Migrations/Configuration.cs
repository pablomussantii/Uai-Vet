namespace Vet.Webside.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Vet.Domain;
    using Vet.Data;
    using Vet.Services;

    internal sealed class Configuration : DbMigrationsConfiguration<Vet.Webside.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Vet.Webside.Models.ApplicationDbContext context)
        {
       
        }
    }
}
