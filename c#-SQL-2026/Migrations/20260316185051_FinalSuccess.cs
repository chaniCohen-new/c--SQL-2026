using System;
using Microsoft.EntityFrameworkCore;
using c__nRepository_2026.Entities;
using c__nRepository_2026.Interfaces;

namespace c__SQL_2026.Identification
{
    public class IdentificationContext : DbContext, IContext
    {
        // בנאי ריק לטובת ה-Migrations
        public IdentificationContext()
        {
        }

        // בנאי עם אפשרויות להזרקה
        public IdentificationContext(DbContextOptions<IdentificationContext> options) : base(options)
        {
        }

        // הגדרת הטבלאות
        public DbSet<Character> Characters { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DetectedCharacter> DetectedCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // הגדרת קשר בין גלריה לדמות
            modelBuilder.Entity<Gallery>()
                .HasOne(g => g.Character)
                .WithMany()
                .HasForeignKey(g => g.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            // הגדרת קשר בין דמות שזוהתה לדמות המקורית
            modelBuilder.Entity<DetectedCharacter>()
                .HasOne(dc => dc.Character)
                .WithMany(c => c.DetectedCharacters)
                .HasForeignKey(dc => dc.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            // הגדרת קשר בין תמונה לגלריה - כאן מותר Cascade
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Gallery)
                .WithMany(g => g.Images)
                .HasForeignKey(i => i.GalleryId)
                .OnDelete(DeleteBehavior.Cascade);

            // התיקון שמונע את ה-Cycle: שינוי ל-Restrict
            modelBuilder.Entity<DetectedCharacter>()
                .HasOne(dc => dc.Image)
                .WithMany(i => i.DetectedCharacters)
                .HasForeignKey(dc => dc.ImageId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // שימי לב שה-Server Name נכון למחשב שלך
                optionsBuilder.UseSqlServer("server=DESKTOP-13C4MS2;database=ImageRecognitionDB;trusted_connection=true;TrustServerCertificate=true");
            }
        }

        // מימוש פונקציית השמירה מהאינטרפייס
        public int Save()
        {
            return base.SaveChanges();
        }
    }
}