namespace EtnoSelo.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aktivnost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Vodic = c.Boolean(nullable: false),
                        Oprema = c.String(),
                        TipNaplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipNaplate", t => t.TipNaplateId)
                .Index(t => t.TipNaplateId);
            
            CreateTable(
                "dbo.OdrzavanjeAktivnosti",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Polazak = c.DateTime(nullable: false),
                        Vodic = c.String(),
                        Trajanje = c.Time(nullable: false, precision: 7),
                        AktivnostId = c.Int(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                        StrucnaOsobaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aktivnost", t => t.AktivnostId)
                .ForeignKey("dbo.Korisnik", t => t.KorisnikId)
                .ForeignKey("dbo.StrucnaOsoba", t => t.StrucnaOsobaId)
                .Index(t => t.AktivnostId)
                .Index(t => t.KorisnikId)
                .Index(t => t.StrucnaOsobaId);
            
            CreateTable(
                "dbo.Korisnik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        KorisnickoIme = c.String(),
                        Lozinka = c.String(),
                        Email = c.String(),
                        DatumRodjenja = c.DateTime(nullable: false),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        Adresa = c.String(),
                        Telefon = c.String(),
                        Aktivan = c.Boolean(nullable: false),
                        RolaId = c.Int(nullable: false),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grad", t => t.GradId)
                .ForeignKey("dbo.Rola", t => t.RolaId)
                .Index(t => t.RolaId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Cijenik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnik", t => t.KorisnikId)
                .Index(t => t.KorisnikId);
            
            CreateTable(
                "dbo.JedinicaCijenik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cijena = c.Single(nullable: false),
                        PoreznaStopa = c.Single(nullable: false),
                        JedinicaId = c.Int(nullable: false),
                        CijenikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cijenik", t => t.CijenikId)
                .ForeignKey("dbo.Jedinica", t => t.JedinicaId)
                .Index(t => t.JedinicaId)
                .Index(t => t.CijenikId);
            
            CreateTable(
                "dbo.Jedinica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Kapacitet = c.Short(nullable: false),
                        Oznaka = c.String(),
                        TipSmjestajaId = c.Int(nullable: false),
                        ObjektId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Objekt", t => t.ObjektId)
                .ForeignKey("dbo.TipSmjestaja", t => t.TipSmjestajaId)
                .Index(t => t.TipSmjestajaId)
                .Index(t => t.ObjektId);
            
            CreateTable(
                "dbo.JedinicaPonuda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cijena = c.Single(nullable: false),
                        PoreznaStopa = c.Single(nullable: false),
                        PonudaId = c.Int(nullable: false),
                        JedinicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jedinica", t => t.JedinicaId)
                .ForeignKey("dbo.Ponuda", t => t.PonudaId)
                .Index(t => t.PonudaId)
                .Index(t => t.JedinicaId);
            
            CreateTable(
                "dbo.Ponuda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                        Popust = c.Single(nullable: false),
                        DatumKreiranja = c.DateTime(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisnik", t => t.KorisnikId)
                .Index(t => t.KorisnikId);
            
            CreateTable(
                "dbo.PonudaAktivnost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PonudaId = c.Int(nullable: false),
                        AktivnostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aktivnost", t => t.AktivnostId)
                .ForeignKey("dbo.Ponuda", t => t.PonudaId)
                .Index(t => t.PonudaId)
                .Index(t => t.AktivnostId);
            
            CreateTable(
                "dbo.Kvar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Opis = c.String(),
                        Naziv = c.String(),
                        Rijesen = c.Boolean(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                        JedinicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jedinica", t => t.JedinicaId)
                .ForeignKey("dbo.Korisnik", t => t.KorisnikId)
                .Index(t => t.KorisnikId)
                .Index(t => t.JedinicaId);
            
            CreateTable(
                "dbo.Objekt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        EtnoSeloId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EtnoSelo", t => t.EtnoSeloId)
                .Index(t => t.EtnoSeloId);
            
            CreateTable(
                "dbo.EtnoSelo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        Kontakt = c.String(),
                        RadnoVrijeme = c.String(),
                        ZiroRacun = c.String(),
                        GradId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grad", t => t.GradId)
                .Index(t => t.GradId);
            
            CreateTable(
                "dbo.Grad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        DrzavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drzava", t => t.DrzavaId)
                .Index(t => t.DrzavaId);
            
            CreateTable(
                "dbo.Drzava",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rezervacija",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumDolaska = c.DateTime(nullable: false),
                        DatumOdlaska = c.DateTime(nullable: false),
                        BrojOsoba = c.Short(nullable: false),
                        Potvrdjena = c.Boolean(nullable: false),
                        DatumPotvrdjivanja = c.DateTime(nullable: false),
                        Komentar = c.String(),
                        Ocjena = c.Short(nullable: false),
                        Popust = c.Single(nullable: false),
                        KorisnikId = c.Int(nullable: false),
                        JedinicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jedinica", t => t.JedinicaId)
                .ForeignKey("dbo.Korisnik", t => t.KorisnikId)
                .Index(t => t.KorisnikId)
                .Index(t => t.JedinicaId);
            
            CreateTable(
                "dbo.Ucesnici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OdgovornaOsoba = c.String(),
                        Oprema = c.String(),
                        BrojOsoba = c.Short(nullable: false),
                        RezervacijaId = c.Int(nullable: false),
                        OdrzavanjeAktivnostiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OdrzavanjeAktivnosti", t => t.OdrzavanjeAktivnostiId)
                .ForeignKey("dbo.Rezervacija", t => t.RezervacijaId)
                .Index(t => t.RezervacijaId)
                .Index(t => t.OdrzavanjeAktivnostiId);
            
            CreateTable(
                "dbo.UslugaRezervacija",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Single(nullable: false),
                        Cijena = c.Single(nullable: false),
                        PoreznaStopa = c.Single(nullable: false),
                        RezervacijaId = c.Int(nullable: false),
                        UslugaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rezervacija", t => t.RezervacijaId)
                .ForeignKey("dbo.Usluga", t => t.UslugaId)
                .Index(t => t.RezervacijaId)
                .Index(t => t.UslugaId);
            
            CreateTable(
                "dbo.Usluga",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Cijena = c.Single(nullable: false),
                        PoreznaStopa = c.Single(nullable: false),
                        TipNaplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipNaplate", t => t.TipNaplateId)
                .Index(t => t.TipNaplateId);
            
            CreateTable(
                "dbo.TipNaplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Oznaka = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipSmjestaja",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rola",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StrucnaOsoba",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImePrezime = c.String(),
                        Zvanje = c.String(),
                        Adresa = c.String(),
                        Telefon = c.String(),
                        Email = c.String(),
                        CertificiraneOblasti = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nivo = c.String(),
                        Poruka = c.String(),
                        Izuzetak = c.String(),
                        Klasa = c.String(),
                        Metoda = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OdrzavanjeAktivnosti", "StrucnaOsobaId", "dbo.StrucnaOsoba");
            DropForeignKey("dbo.Korisnik", "RolaId", "dbo.Rola");
            DropForeignKey("dbo.OdrzavanjeAktivnosti", "KorisnikId", "dbo.Korisnik");
            DropForeignKey("dbo.Cijenik", "KorisnikId", "dbo.Korisnik");
            DropForeignKey("dbo.Jedinica", "TipSmjestajaId", "dbo.TipSmjestaja");
            DropForeignKey("dbo.UslugaRezervacija", "UslugaId", "dbo.Usluga");
            DropForeignKey("dbo.Usluga", "TipNaplateId", "dbo.TipNaplate");
            DropForeignKey("dbo.Aktivnost", "TipNaplateId", "dbo.TipNaplate");
            DropForeignKey("dbo.UslugaRezervacija", "RezervacijaId", "dbo.Rezervacija");
            DropForeignKey("dbo.Ucesnici", "RezervacijaId", "dbo.Rezervacija");
            DropForeignKey("dbo.Ucesnici", "OdrzavanjeAktivnostiId", "dbo.OdrzavanjeAktivnosti");
            DropForeignKey("dbo.Rezervacija", "KorisnikId", "dbo.Korisnik");
            DropForeignKey("dbo.Rezervacija", "JedinicaId", "dbo.Jedinica");
            DropForeignKey("dbo.Jedinica", "ObjektId", "dbo.Objekt");
            DropForeignKey("dbo.Objekt", "EtnoSeloId", "dbo.EtnoSelo");
            DropForeignKey("dbo.Korisnik", "GradId", "dbo.Grad");
            DropForeignKey("dbo.EtnoSelo", "GradId", "dbo.Grad");
            DropForeignKey("dbo.Grad", "DrzavaId", "dbo.Drzava");
            DropForeignKey("dbo.Kvar", "KorisnikId", "dbo.Korisnik");
            DropForeignKey("dbo.Kvar", "JedinicaId", "dbo.Jedinica");
            DropForeignKey("dbo.PonudaAktivnost", "PonudaId", "dbo.Ponuda");
            DropForeignKey("dbo.PonudaAktivnost", "AktivnostId", "dbo.Aktivnost");
            DropForeignKey("dbo.Ponuda", "KorisnikId", "dbo.Korisnik");
            DropForeignKey("dbo.JedinicaPonuda", "PonudaId", "dbo.Ponuda");
            DropForeignKey("dbo.JedinicaPonuda", "JedinicaId", "dbo.Jedinica");
            DropForeignKey("dbo.JedinicaCijenik", "JedinicaId", "dbo.Jedinica");
            DropForeignKey("dbo.JedinicaCijenik", "CijenikId", "dbo.Cijenik");
            DropForeignKey("dbo.OdrzavanjeAktivnosti", "AktivnostId", "dbo.Aktivnost");
            DropIndex("dbo.Usluga", new[] { "TipNaplateId" });
            DropIndex("dbo.UslugaRezervacija", new[] { "UslugaId" });
            DropIndex("dbo.UslugaRezervacija", new[] { "RezervacijaId" });
            DropIndex("dbo.Ucesnici", new[] { "OdrzavanjeAktivnostiId" });
            DropIndex("dbo.Ucesnici", new[] { "RezervacijaId" });
            DropIndex("dbo.Rezervacija", new[] { "JedinicaId" });
            DropIndex("dbo.Rezervacija", new[] { "KorisnikId" });
            DropIndex("dbo.Grad", new[] { "DrzavaId" });
            DropIndex("dbo.EtnoSelo", new[] { "GradId" });
            DropIndex("dbo.Objekt", new[] { "EtnoSeloId" });
            DropIndex("dbo.Kvar", new[] { "JedinicaId" });
            DropIndex("dbo.Kvar", new[] { "KorisnikId" });
            DropIndex("dbo.PonudaAktivnost", new[] { "AktivnostId" });
            DropIndex("dbo.PonudaAktivnost", new[] { "PonudaId" });
            DropIndex("dbo.Ponuda", new[] { "KorisnikId" });
            DropIndex("dbo.JedinicaPonuda", new[] { "JedinicaId" });
            DropIndex("dbo.JedinicaPonuda", new[] { "PonudaId" });
            DropIndex("dbo.Jedinica", new[] { "ObjektId" });
            DropIndex("dbo.Jedinica", new[] { "TipSmjestajaId" });
            DropIndex("dbo.JedinicaCijenik", new[] { "CijenikId" });
            DropIndex("dbo.JedinicaCijenik", new[] { "JedinicaId" });
            DropIndex("dbo.Cijenik", new[] { "KorisnikId" });
            DropIndex("dbo.Korisnik", new[] { "GradId" });
            DropIndex("dbo.Korisnik", new[] { "RolaId" });
            DropIndex("dbo.OdrzavanjeAktivnosti", new[] { "StrucnaOsobaId" });
            DropIndex("dbo.OdrzavanjeAktivnosti", new[] { "KorisnikId" });
            DropIndex("dbo.OdrzavanjeAktivnosti", new[] { "AktivnostId" });
            DropIndex("dbo.Aktivnost", new[] { "TipNaplateId" });
            DropTable("dbo.Log");
            DropTable("dbo.StrucnaOsoba");
            DropTable("dbo.Rola");
            DropTable("dbo.TipSmjestaja");
            DropTable("dbo.TipNaplate");
            DropTable("dbo.Usluga");
            DropTable("dbo.UslugaRezervacija");
            DropTable("dbo.Ucesnici");
            DropTable("dbo.Rezervacija");
            DropTable("dbo.Drzava");
            DropTable("dbo.Grad");
            DropTable("dbo.EtnoSelo");
            DropTable("dbo.Objekt");
            DropTable("dbo.Kvar");
            DropTable("dbo.PonudaAktivnost");
            DropTable("dbo.Ponuda");
            DropTable("dbo.JedinicaPonuda");
            DropTable("dbo.Jedinica");
            DropTable("dbo.JedinicaCijenik");
            DropTable("dbo.Cijenik");
            DropTable("dbo.Korisnik");
            DropTable("dbo.OdrzavanjeAktivnosti");
            DropTable("dbo.Aktivnost");
        }
    }
}
