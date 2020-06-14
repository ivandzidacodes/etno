using EtnoSelo.DAL.Models;
using EtnoSelo.DAL.Shared;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnoSelo.DAL
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(string connectionString = Constants.CONNECTION_STRING) : base($"name={connectionString}") { }

        public DbSet<Aktivnost> Aktivnost { get; set; }
        public DbSet<Cijenik> Cijenik { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<EtnoSelo.DAL.Models.EtnoSelo> EtnoSelo { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Jedinica> Jedinica { get; set; }
        public DbSet<JedinicaCijenik> JedinicaCijenik { get; set; }
        public DbSet<JedinicaPonuda> JedinicaPonuda { get; set; }
        public DbSet<Kvar> Kvar { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Objekt> Objekt { get; set; }
        public DbSet<OdrzavanjeAktivnosti> OdrzavanjeAktivnosti { get; set; }
        public DbSet<Ponuda> Ponuda { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Models.Rola> Rola { get; set; }
        public DbSet<StrucnaOsoba> StrucnaOsoba { get; set; }
        public DbSet<TipNaplate> TipNaplate { get; set; }
        public DbSet<TipSmjestaja> TipSmjestaja { get; set; }
        public DbSet<Ucesnici> Ucesnici { get; set; }
        public DbSet<Usluga> Usluga { get; set; }
        public DbSet<UslugaRezervacija> UslugaRezervacija { get; set; }
        public DbSet<Log> Logovi { get; set; }
        public DbSet<PonudaAktivnost> PonudeAktivnosti { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
