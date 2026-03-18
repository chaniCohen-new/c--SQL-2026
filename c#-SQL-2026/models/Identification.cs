using System;
using Microsoft.EntityFrameworkCore;
using c__nRepository_2026.Entities;
using c__nRepository_2026.Interfaces;

namespace c__SQL_2026.Identification
{
    // שינינו את השם ל-IdentificationContext
    public class IdentificationContext : DbContext, IContext
    {
        public IdentificationContext()
        {
        }

        public IdentificationContext(DbContextOptions<IdentificationContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DetectedCharacter> DetectedCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // ... שאר הקוד של המודל נשאר אותו דבר ...
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-13C4MS2;database=ImageRecognitionDB;trusted_connection=true;TrustServerCertificate=true");
            }
        }

        public int Save()
        {
            return base.SaveChanges();
        }
    }
}