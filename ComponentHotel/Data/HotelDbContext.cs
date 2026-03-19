using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentHotelCL;
using Microsoft.EntityFrameworkCore;

namespace ComponentHotel.Data
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Gast> Gasten { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Hotelkamer> Hotelkamers { get; set; }
        public DbSet<Reservering> Reserveringen { get; set; }
        public DbSet<ReserveringHotelkamer> ReserveringHotelkamers { get; set; }
        public DbSet<Tarief> Tarieven { get; set; }



        // SQL Server connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=.;Initial Catalog=HotelDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gast
            modelBuilder.Entity<Gast>(entity =>
            {
                entity.ToTable("Gasten");

                entity.HasKey(g => g.GastId);

                // Properties
                entity.Property(g => g.Email)
                      .HasMaxLength(256)
                      .IsUnicode(false);

                entity.Property(g => g.Voornaam)
                      .HasMaxLength(100)
                      .IsUnicode(true)
                      .IsRequired(false);

                entity.Property(g => g.Achternaam)
                      .HasMaxLength(100)
                      .IsUnicode(true)
                      .IsRequired(false);

                // Telefoonnummer should be string in the model; store as varchar(20)
                entity.Property(g => g.Telefoonnummer)
                      .HasMaxLength(20)
                      .IsUnicode(false)
                      .HasColumnName("Telefoonnummer");

                // Address fields
                entity.Property(g => g.Straat)
                      .HasColumnName("Straat")
                      .HasMaxLength(200)
                      .IsUnicode(true);

                entity.Property(g => g.Huisnummer)
                      .HasMaxLength(20)
                      .IsUnicode(false);

                entity.Property(g => g.Postcode)
                      .HasMaxLength(20)
                      .IsUnicode(false);

                entity.Property(g => g.Woonplaats)
                      .HasMaxLength(100)
                      .IsUnicode(true);

                entity.Property(g => g.Land)
                      .HasMaxLength(100)
                      .IsUnicode(true);

                // IBAN: max length 34, store as non-unicode
                entity.Property(g => g.IBAN)
                      .HasMaxLength(34)
                      .IsUnicode(false);

                // Indexes
                entity.HasIndex(g => g.Email)
                      .HasDatabaseName("IX_Gast_Email");
            });

            // Gebruiker
            modelBuilder.Entity<Gebruiker>(entity =>
            {
                entity.ToTable("Gebruikers");

                entity.HasKey(u => u.GebruikerId);

                // Relationship to Gast
                entity.HasOne(u => u.Gast)
                      .WithMany()
                      .HasForeignKey(u => u.GastId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(u => u.Email)
                      .HasMaxLength(256)
                      .IsUnicode(false);

                entity.Property(u => u.WachtwoordHash)
                      .HasMaxLength(512)
                      .IsUnicode(false);

                entity.Property(u => u.Rol)
                      .HasMaxLength(50)
                      .IsUnicode(false);
                
            });

            // Hotelkamer
            modelBuilder.Entity<Hotelkamer>(entity =>
            {
                entity.ToTable("Hotelkamers");

                entity.HasKey(h => h.HotelkamerId);

                entity.Property(h => h.Kamernummer)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(h => h.Type)
                      .HasMaxLength(100)
                      .IsUnicode(true);

                entity.Property(h => h.PrijsPerNacht)
                      .HasColumnType("decimal(18,2)");

                entity.Property(h => h.IsBeschikbaar)
                      .HasDefaultValue(true);

            });

            // Reservering
            modelBuilder.Entity<Reservering>(entity =>
            {
                entity.ToTable("Reserveringen");

                entity.HasKey(r => r.ReserveringId);

                entity.Property(r => r.IncheckDatum)
                      .IsRequired();

                entity.Property(r => r.UitcheckDatum)
                      .IsRequired();

                entity.Property(r => r.Status)
                      .IsRequired();

                // Relationships
                entity.HasOne(r => r.Gast)
                      .WithMany()
                      .HasForeignKey(r => r.GastId);


                entity.HasOne(r => r.Tarief)
                        .WithMany()
                        .HasForeignKey(r => r.TariefId);
                

                entity.HasMany(r => r.Hotelkamers)
                      .WithOne(rk => rk.Reservering)
                      .HasForeignKey(rk => rk.ReserveringId);

            });

            modelBuilder.Entity<ReserveringHotelkamer>(entity =>
            {
                entity.ToTable("ReserveringHotelkamers");

                entity.HasKey(rk => new { rk.ReserveringId, rk.HotelkamerId });

                entity.HasOne(rk => rk.Reservering)
                      .WithMany(r => r.Hotelkamers)
                      .HasForeignKey(rk => rk.ReserveringId);

                entity.HasOne(rk => rk.Hotelkamer)
                      .WithMany(h => h.ReserveringHotelkamers)
                      .HasForeignKey(rk => rk.HotelkamerId);
            });

            modelBuilder.Entity<Tarief>(entity =>
            {
                entity.ToTable("Tarieven");

                entity.HasKey(t => t.TariefId);

                entity.Property(t => t.Subtotaal)
                      .HasColumnType("decimal(18,2)");
                
                entity.Property(t => t.KortingPercentage)
                      .HasColumnType("decimal(5,2)");
                
                entity.Property(t => t.BedragNaKorting)
                      .HasColumnType("decimal(18,2)");
                
                entity.Property(t => t.ToeristenbelastingPercentage)
                      .HasColumnType("decimal(5,2)");
                
                entity.Property(t => t.TotaalBedrag)
                      .HasColumnType("decimal(18,2)");
                
                entity.Property(t => t.BerekendOp)
                      .IsRequired();
                // Relationship with Reservering
                entity.HasOne(t => t.Reservering)
                      .WithMany()
                      .HasForeignKey(t => t.ReserveringId);
                      
            });



            // Data seed
            modelBuilder.Entity<Gast>().HasData(
                new Gast
                {
                    GastId = 1,
                    Email = "jan@example.com",
                    Voornaam = "Jan",
                    Achternaam = "Jansen",
                    Telefoonnummer = "0612345678",
                    Straat = "Hoofdstraat",
                    Huisnummer = "1A",
                    Postcode = "1234AB",
                    Woonplaats = "Amsterdam",
                    Land = "Netherlands",
                    IBAN = "NL91ABNA0417164300"
                }
            );

            modelBuilder.Entity<Hotelkamer>().HasData(
                new Hotelkamer {
                    HotelkamerId = 1,
                    Kamernummer = "101",
                    Type = "Standaard",
                    PrijsPerNacht = 75.00m,
                    IsBeschikbaar = true
                },
                new Hotelkamer{
                    HotelkamerId = 2, 
                    Kamernummer = "102", 
                    Type = "Comfort", 
                    PrijsPerNacht = 95.50m, 
                    IsBeschikbaar = true 
                }
            );

            modelBuilder.Entity<Tarief>().HasData(
                new Tarief
                {
                    TariefId = 1,
                    ReserveringId = 1,
                    Subtotaal = 300.00m,
                    KortingPercentage = 10.00m,
                    BedragNaKorting = 270.00m,
                    ToeristenbelastingPercentage = 5.00m,
                    TotaalBedrag = 283.50m,
                    BerekendOp = new DateTime(2025, 6, 5)
                }
            );

            modelBuilder.Entity<Reservering>().HasData(
                new Reservering
                {
                    ReserveringId = 1,
                    GastId = 1,
                    IncheckDatum = new DateTime(2025, 6, 1),
                    UitcheckDatum = new DateTime(2025, 6, 5),
                    TariefId = 1,
                    Status = true
                }
            );

            modelBuilder.Entity<ReserveringHotelkamer>().HasData(
                new
                {
                    ReserveringId = 1,
                    HotelkamerId = 1
                }
            );

            modelBuilder.Entity<Gebruiker>().HasData(
                // Replace WachtwoordHash with a real hashed password before deploying
                new Gebruiker
                {
                    GebruikerId = 1,
                    GastId = 1,
                    Email = "admin@example.com",
                    WachtwoordHash = "REPLACE_WITH_HASH",
                    Rol = "Admin"
                }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
