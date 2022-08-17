using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Models;
using DisneyApi.Services.DbService;

namespace DisneyApiProject.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MoviesSeriesController : ControllerBase
    {
        private readonly DisneyService<MovieSerie> _disneyMoviesService;

        public MoviesSeriesController(DisneyService<MovieSerie> disneyService)
        {
            _disneyMoviesService = disneyService;
        }

        // GET: api/MoviesSeries
        [HttpGet]
        public async Task<ActionResult<List<MovieSerie>>> GetMedia([FromQuery] int? id, [FromQuery] string? title, [FromQuery] int? rate, [FromQuery] bool? asc)
        {
            var moviesSeries = await _disneyMoviesService.GetAllAsync();

            if (moviesSeries is null) return NoContent();

            if (id is not null) return await MovieSerieExists((int)id) ? moviesSeries.Where(x => x.MovieSerie_Id == id).ToList() : NoContent(); 
            
            if (title is not null)
            {
                moviesSeries = moviesSeries.Where(x => x.Title == title) as ICollection<MovieSerie>;
            }

            if (rate is not null)
            {
                moviesSeries = moviesSeries.Where(x => x.Rate == rate) as ICollection<MovieSerie>;
            }

            if (asc is not null)
            {
                if ((bool)asc)
                {
                    moviesSeries = moviesSeries.OrderBy(x => x.Title) as ICollection<MovieSerie>;
                }
                else
                {
                    moviesSeries = moviesSeries.OrderByDescending(x => x.Title) as ICollection<MovieSerie>;
                }
            }

            return moviesSeries.Count > 0 ? moviesSeries.ToList() : NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutMovieSerie(MovieSerie movieSerie)
        {
            if (movieSerie is null || await MovieSerieExists(movieSerie.MovieSerie_Id))
            {
                return BadRequest();
            }

            return Ok(await _disneyMoviesService.InsertAsync(movieSerie));
        }

        [HttpPost]
        public async Task<ActionResult<MovieSerie>> PostMovieSerie(MovieSerie movieSerie)
        {
            if (movieSerie is null) return BadRequest();

            return await MovieSerieExists(movieSerie.MovieSerie_Id) ? 
                 await _disneyMoviesService.UpdateAsync(movieSerie) : 
                 await _disneyMoviesService.InsertAsync(movieSerie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieSerie(int id)
        {
            if (await MovieSerieExists(id) is false)
            {
                return BadRequest();
            }

            return Ok(_disneyMoviesService.DeleteAsync(id));
        }

        private async Task<bool> MovieSerieExists(int id)
        {
            return await _disneyMoviesService.GetAsync(id) is not null;
        }
    }
}
