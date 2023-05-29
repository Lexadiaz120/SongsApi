using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApiDbContext : DbContext
    { 
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { 
        


        }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
                new Song
                {

                    Id = 1, 
                    Title = "Willow", 
                    Language = "en", 
                    Duration = "4:30"
                },
                 new Song
                 {

                     Id = 2,
                     Title = "Willow2",
                     Language = "es",
                     Duration = "4:40"
                 }
                );
        }

    }
}
