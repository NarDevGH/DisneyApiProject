using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Models;
using DisneyApi.Services.DbService;

namespace DisneyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesSeriesController : ControllerBase
    {
        private readonly DisneyService<MovieSerie> _disneyService;

        public MoviesSeriesController(DisneyService<MovieSerie> disneyService)
        {
            _disneyService = disneyService;
        }

        // GET: api/MoviesSeries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieSerie>>> GetMedia()
        {
            throw new NotImplementedException();
        }

        // GET: api/MoviesSeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieSerie>> GetMovieSerie(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/MoviesSeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieSerie(int id, MovieSerie movieSerie)
        {
            throw new NotImplementedException();
        }

        // POST: api/MoviesSeries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieSerie>> PostMovieSerie(MovieSerie movieSerie)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/MoviesSeries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieSerie(int id)
        {
            throw new NotImplementedException();
        }

        private bool MovieSerieExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
