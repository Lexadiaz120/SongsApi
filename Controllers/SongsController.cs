using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace SongsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext; 

         
        [HttpGet] 

        public async Task<IActionResult> GetAllSongs(int ? pageNumber, int? pageSize)
        {

            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5; 
            var songs =  from song in _dbContext.Songs
                        select new
                        {
                            Id = song.Id,
                            Title = song.Title,
                            Duration = song.Duration,
                            ImageUrl = song.ImageUrl,
                            AudioUrl = song.AudioUrl
                        }.ToListAsync();
            return Ok(songs.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult>  FeautedSongs()
        {
            var songs =  await  (from song in _dbContext.Songs
                                 where song.IsFeatured = true 
                        select new
                        {
                            Id = song.Id,
                            Title = song.Title,
                            Duration = song.Duration,
                            ImageUrl = song.ImageUrl,
                            AudioUrl = song.AudioUrl
                        }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from song in _dbContext.Songs
                               orderby song.UploadedDate descending 
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl
                               }).Take(15).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string query)
        {
            var songs = await (from song in _dbContext.Songs
                               where song.Title.StartsWith(query)
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl
                               }).Take(15).ToListAsync();
            return Ok(songs);
        }

    }
}
