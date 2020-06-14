namespace EtnoSelo.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rezervacija", "PonudaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rezervacija", "PonudaId");
            AddForeignKey("dbo.Rezervacija", "PonudaId", "dbo.Ponuda", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rezervacija", "PonudaId", "dbo.Ponuda");
            DropIndex("dbo.Rezervacija", new[] { "PonudaId" });
            DropColumn("dbo.Rezervacija", "PonudaId");
        }
    }
}
