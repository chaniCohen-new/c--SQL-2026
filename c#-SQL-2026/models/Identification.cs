using Microsoft.EntityFrameworkCore;
using C__repository_2026.Entities; // אנחנו מייבאים את המודלים מהתיקייה/פרויקט שלך

namespace C__SQL_2026.models // תשני את ה-namespace בהתאם למיקום הקובץ שלך
{
    public class AppDbContext : DbContext
    {
        // הבנאי שמקבל את הגדרות החיבור (כמו מחרוזת החיבור מתוך ה-appsettings.json)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // הגדרת הטבלאות שייווצרו ב-SQL:
        public DbSet<Character> Characters { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DetectedCharacter> DetectedCharacters { get; set; } // טבלת הגישור שלנו

        // כאן אנחנו מגדירים ל-SQL הגדרות מתקדמות על הקשרים בין הטבלאות
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. הגדרת הקשר בין טבלת הגישור (DetectedCharacter) לתמונה
            modelBuilder.Entity<DetectedCharacter>()
                .HasOne(dc => dc.Image)
                .WithMany(i => i.DetectedCharacters)
                .HasForeignKey(dc => dc.ImageId)
                .OnDelete(DeleteBehavior.Cascade); // משמעות: אם מוחקים תמונה מהאתר, ימחקו אוטומטית גם כל שורות הזיהוי שלה

            // 2. הגדרת הקשר בין טבלת הגישור לדמות
            modelBuilder.Entity<DetectedCharacter>()
                .HasOne(dc => dc.Character)
                .WithMany(c => c.DetectedCharacters)
                .HasForeignKey(dc => dc.CharacterId)
                .OnDelete(DeleteBehavior.Cascade); // אם מוחקים דמות, ימחקו הזיהויים שלה

            // 3. הגדרת קשר בין תמונה לגלריה
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Gallery)
                .WithMany(g => g.Images)
                .HasForeignKey(i => i.GalleryId)
                .OnDelete(DeleteBehavior.Cascade); // אם מוחקים גלריה, כל התמונות שבתוכה נמחקות
