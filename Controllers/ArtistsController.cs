using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using SongsApi.Models;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace SongsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    { 
        private ApiDbContext _dbContext;
        public ArtistsController(ApiDbContext dbContext )
        {
            _dbContext = dbContext; 
        }

        [HttpPost] 

        public async Task<IActionResult> Post([FromForm] Artist artist)
        {
            var imageUrl = await FileHelper.UploadImage(Artist.image);
            artist.ImageUrl = imageUrl; 
            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);  
        }

        [HttpGet] 

        public async Task <IActionResult> GetArtists()
        {
           var artists = await (from artist in _dbContext.Artists
                                select new
                                {
                                  Id = artist.Id,
                                  Name = artist.Name,
                                }).ToListAsync();

            return Ok(artists);
        }

    }
}
