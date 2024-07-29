// Data/AppDbContext.cs
using firstApp.Models;
using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext   : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext > options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PId);
        }
            // Configure the model if needed
    }
