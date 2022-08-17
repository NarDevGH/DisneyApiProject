using Microsoft.AspNetCore.Mvc;
using DisneyApi.Models;
using DisneyApi.Services.DbService;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace DisneyApi.Controllers
{
    [Route("characters")]
    [ApiController]
    [Authorize]
    public class CharactersController : ControllerBase
    {
        private readonly DisneyService<Character> _disneyCharacters_Service;

        public CharactersController(DisneyService<Character> disneyCharacters_Service)
        {
            _disneyCharacters_Service = disneyCharacters_Service;
        }



        [HttpGet]
        public async Task<ActionResult<List<Character>>> QueryCharacters([FromQuery] int? id, [FromQuery]  string? name, [FromQuery] int? age, [FromQuery] float? weight, [FromQuery] int? movieSerie_Id)
        {
            var characters = (await _disneyCharacters_Service.GetAllAsync()).ToList();
        
            if (characters is null)
            {
                return NoContent();
            }
            
            if (id is not null)
            {
                return await CharacterExists((int)id) ? characters.Where(x => x.Character_Id == id).ToList() : NoContent();
            }
        
            if (name is not null)
            {
                characters = characters.Where(x => x.Name == name).ToList();
            }
            if (age is not null)
            {
                characters = characters.Where(x => x.Age == age).ToList();
            }
            if (weight is not null)
            {
                characters = characters.Where(x => x.Weight == weight).ToList();
            }
            if (movieSerie_Id is not null)
            {
                characters = characters.Where(x => x.MoviesAndSeries.Any(y => y.MovieSerie_Id == movieSerie_Id)).ToList();
            }
        
            return characters.Count() > 0 ? characters : NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutCharacter(Character character)
        {
            if (character is null || await CharacterExists(character.Character_Id))
            {
                return BadRequest();
            }

            await _disneyCharacters_Service.InsertAsync(character);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            if (character is null)
            {
                return BadRequest();
            }

            return await _disneyCharacters_Service.InsertAsync(character);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (await _disneyCharacters_Service.GetAsync(id) is null)
            {
                return BadRequest();
            }

            await _disneyCharacters_Service.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CharacterExists(int id)
        {
            return ( await _disneyCharacters_Service.GetAsync(id) ) is not null;
        }
    }
}