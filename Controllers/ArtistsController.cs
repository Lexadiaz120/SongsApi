using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using SongsApi.Models;
using System.Reflection.Metadata.Ecma335;
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
        [HttpGet("[action]")]
        public async Task <IActionResult>  ArtistDetails(int artistId) {

            // SELECT artists_title, artist_name FROM  INNER JOIN Artists ON Artists.artistId =  Song.artistId WHERE ; 
            var artistDetails = await _dbContext.Artists.Where(a => a.Id == artistId).Include(a => a.Songs).ToListAsync();   
            return Ok(artistDetails);       
        }

        [HttpGet] 

        public async Task <IActionResult> GetAlbums()
        {
            var albums = await (from album in _dbContext.Albums select new
            {
               Id = album.id, 
               Name = album.name,
               ImageUrl = album.ImageUrl
            }).ToListAsync();

            return Ok(albums);
        }

        [HttpGet("[action]")]
       
        public async Task <IActionResult> AlbumDetails(int albumId)
        {
            var albumDetails = await  _dbContext.Albums.Where(a => a.Id == albumId).Include(a => a.Songs).ToListAsync();  
            return Ok(albumDetails);
        }

    }
}
