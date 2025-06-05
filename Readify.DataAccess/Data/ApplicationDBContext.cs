using Microsoft.EntityFrameworkCore;
using Readify.Models;

namespace Readify.DataAccess.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category { Id = 1,Name ="Fiction", DisplayOrder = 1 },
                new Category { Id = 2,Name ="History", DisplayOrder = 2 },
                new Category { Id = 3,Name ="Science", DisplayOrder = 3 },
                new Category { Id = 4,Name ="Philosophy", DisplayOrder = 4},
            });

        }
    }
}
