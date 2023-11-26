using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "scifi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "history", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "novel", DisplayOrder = 4 }
                );
        }
    }
}
