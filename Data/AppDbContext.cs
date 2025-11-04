using Microsoft.EntityFrameworkCore;
using ScamWarning.Models;

namespace ScamWarning.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warning> Warnings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
                entity.Property(u => u.IsAdmin).HasDefaultValue(false);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Description).HasMaxLength(500);
            });

            // Warning Configuration
            modelBuilder.Entity<Warning>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Title).IsRequired().HasMaxLength(200);
                entity.Property(w => w.Description).IsRequired();
                entity.Property(w => w.WarningSigns).IsRequired();
                entity.Property(w => w.ImageUrl).HasMaxLength(500);
                entity.Property(w => w.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(w => w.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(w => w.Author)
                    .WithMany(u => u.Warnings)
                    .HasForeignKey(w => w.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(w => w.Category)
                    .WithMany(c => c.Warnings)
                    .HasForeignKey(w => w.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Comment Configuration
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Text).IsRequired().HasMaxLength(1000);
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(c => c.Warning)
                    .WithMany(w => w.Comments)
                    .HasForeignKey(c => c.WarningId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Phishing", Description = "Email and website scams that steal personal information" },
                new Category { Id = 2, Name = "Phone Scam", Description = "Fraudulent phone calls and SMS messages" },
                new Category { Id = 3, Name = "Investment Scam", Description = "Fake investment opportunities and Ponzi schemes" },
                new Category { Id = 4, Name = "Romance Scam", Description = "Online dating and romance fraud" },
                new Category { Id = 5, Name = "Other", Description = "Other types of scams" }
            );
        }
    }
}