// Ensure you have the following NuGet package installed in your project:
// Microsoft.EntityFrameworkCore

using Microsoft.EntityFrameworkCore;
using todo.Server.Models.Auth;
using todo.Server.Models.Todo;

namespace todo.Server.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Todos> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();

            modelBuilder.Entity<Todos>().ToTable("Todos");
            modelBuilder.Entity<Todos>().HasKey(t => t.Id);
            modelBuilder.Entity<Todos>().Property(t => t.Task).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Todos>().Property(t => t.UserId).IsRequired();
        }
    }
}
