// Ensure you have the following NuGet package installed in your project:
// Microsoft.EntityFrameworkCore

using Microsoft.EntityFrameworkCore;
using todo.Server.Models.Todo;

namespace todo.Server.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public  DbSet<Todos> Todos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todos>().ToTable("Todos");
            modelBuilder.Entity<Todos>().HasKey(t => t.Id);
            modelBuilder.Entity<Todos>().Property(t => t.Task).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Todos>().HasData(
                new Todos { Id = 1, Task = "Task 1" },
                new Todos { Id = 2, Task = "Task 2" },
                new Todos { Id = 3, Task = "Task 3" }
            );
        }
    }
}
