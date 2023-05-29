using Microsoft.EntityFrameworkCore;
using SongsApi.Models;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApiDbContext : DbContext
    { 
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { 
        


        }
        public DbSet<Song> Songs { get; set; } 
        public DbSet<Artist> Artists { get; set; }  
        public DbSet<Album> Albums { get; set; }

        }

    
}
