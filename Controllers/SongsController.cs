using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        // GET: api/<SongsController> 
        private ApiDbContext _dbContext;  


        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok( await  _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
          var song =  _dbContext.Songs.FindAsync(id);
            if(song == null)
            {
                return NotFound("No record found against this id ");
            }
          return Ok(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] Song song)
        {
            _dbContext.Songs.AddAsync(song); 
            _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songObj)
        {
         var song =   await  _dbContext.Songs.FindAsync(id); 
          if(song == null)
            {
                return NotFound("No record found");
            }else
            {
              song.Title = songObj.Title;
              song.Language = songObj.Language;
              _dbContext.SaveChangesAsync();
              return Ok("Record updated succesfully");
            }
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(int id)
        {
            var song =  await _dbContext.Songs.FindAsync(id); 
            if(song == null)
            {
                return NotFound("No record found against this id");
            } 
            else
            {
              _dbContext.Songs.Remove(song);
              await   _dbContext.SaveChangesAsync();
                return Ok("Record Deleted");
            }
            
        }
    }
}
