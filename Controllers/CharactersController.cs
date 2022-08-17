using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DisneyApi.Services.DbService;
using DisneyApi.Models;
using DisneyApiProject.Dto;

namespace DisneyApi.Controllers
{
    [Route("characters")]
    [ApiController]
    [Authorize]
    public class CharactersController : ControllerBase
    {
        private readonly DisneyService<Character> _disneyCharactersService;

        public CharactersController(DisneyService<Character> disneyCharacters_Service)
        {
            _disneyCharactersService = disneyCharacters_Service;
        }



        [HttpGet]
        public async Task<ActionResult<List<CharactersDto>>> QueryCharacters([FromQuery] int? id, [FromQuery]  string? name, [FromQuery] int? age, [FromQuery] float? weight, [FromQuery] int? movieSerie_Id)
        {
            var characters = await _disneyCharactersService.GetAllAsync();
        
            if (characters is null)
            {
                return NoContent();
            }
            
            if (id is not null)
            {
                return await CharacterExists((int)id) ? characters.Where(x => x.Character_Id == id).Select(x => x.asCharactersDto()).ToList() : NoContent();
            }
        
            if (name is not null)
            {
                characters = characters.Where(x => x.Name == name) as ICollection<Character>;
            }
            if (age is not null)
            {
                characters = characters.Where(x => x.Age == age) as ICollection<Character>;
            }
            if (weight is not null)
            {
                characters = characters.Where(x => x.Weight == weight) as ICollection<Character>;
            }
            if (movieSerie_Id is not null)
            {
                characters = characters.Where(x => x.MoviesAndSeries.Any(y => y.MovieSerie_Id == movieSerie_Id)) as ICollection<Character>;
            }
        
            return characters.Count > 0 ? characters.Select(x => x.asCharactersDto()).ToList() : NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutCharacter(Character character)
        {
            if (character is null || await CharacterExists(character.Character_Id))
            {
                return BadRequest();
            }

            return Ok(await _disneyCharactersService.InsertAsync(character));
        }

        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            if (character is null)
            {
                return BadRequest();
            }

            return await CharacterExists(character.Character_Id) ? 
                await _disneyCharactersService.UpdateAsync(character) : 
                await _disneyCharactersService.InsertAsync(character);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (await _disneyCharactersService.GetAsync(id) is null)
            {
                return BadRequest();
            }

            return Ok(_disneyCharactersService.DeleteAsync(id));
        }

        private async Task<bool> CharacterExists(int id)
        {
            return ( await _disneyCharactersService.GetAsync(id) ) is not null;
        }
    }
}