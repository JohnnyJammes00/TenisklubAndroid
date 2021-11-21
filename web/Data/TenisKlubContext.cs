using Microsoft.EntityFrameworkCore;
using web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class TenisKlubContext : IdentityDbContext<ApplicationUser>
    {
        public TenisKlubContext (DbContextOptions<TenisKlubContext> options): base(options)
        {
        }

        public DbSet<Igralec> Igralci { get; set; }
        public DbSet<Igrisce> Igrisca { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Skupina> Skupine { get; set; }
        public DbSet<IgralecSkupina> IgralecSkupine { get; set; }
        public DbSet<Tekma> Tekme { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Igralec>().ToTable("Igralec");
            modelBuilder.Entity<Igrisce>().ToTable("Igrisce");
            modelBuilder.Entity<Rezervacija>().ToTable("Rezervacija");
            modelBuilder.Entity<Skupina>().ToTable("Skupina");
            modelBuilder.Entity<IgralecSkupina>().ToTable("IgralecSkupina");
            modelBuilder.Entity<Tekma>().ToTable("Tekma");
        }
    }
}