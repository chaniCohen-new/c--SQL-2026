using C__repository_2026.Entities;
using C__repository_2026.Interfaces; // הורדתי את הנקודה המיותרת שהייתה פה!
using Microsoft.EntityFrameworkCore;

namespace C__SQL_2026.models
{
    public class AppDbContext : DbContext, IContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DetectedCharacter> DetectedCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gallery>()
                .HasOne(g => g.Character)
                .WithMany()
                .HasForeignKey(g => g.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetectedCharacter>()
                .HasOne(dc => dc.Character)
                .WithMany(c => c.DetectedCharacters)
                .HasForeignKey(dc => dc.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Gallery)
                .WithMany(g => g.Images)
                .HasForeignKey(i => i.GalleryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetectedCharacter>()
                .HasOne(dc => dc.Image)
                .WithMany(i => i.DetectedCharacters)
                .HasForeignKey(dc => dc.ImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}