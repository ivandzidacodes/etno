namespace EtnoSelo.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dinoTest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Korisnik", "Ime", c => c.String(nullable: false));
            AlterColumn("dbo.Korisnik", "Prezime", c => c.String(nullable: false));
            AlterColumn("dbo.Korisnik", "KorisnickoIme", c => c.String(nullable: false));
            AlterColumn("dbo.Korisnik", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Korisnik", "Adresa", c => c.String(nullable: false));
            AlterColumn("dbo.Korisnik", "Telefon", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Korisnik", "Telefon", c => c.String());
            AlterColumn("dbo.Korisnik", "Adresa", c => c.String());
            AlterColumn("dbo.Korisnik", "Email", c => c.String());
            AlterColumn("dbo.Korisnik", "KorisnickoIme", c => c.String());
            AlterColumn("dbo.Korisnik", "Prezime", c => c.String());
            AlterColumn("dbo.Korisnik", "Ime", c => c.String());
        }
    }
}
