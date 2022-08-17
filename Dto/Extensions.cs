using DisneyApi.Models;

namespace DisneyApiProject.Dto
{
    public static class Extensions
    {
        public static CharactersDto asCharactersDto(this Character character)
        {
            return new CharactersDto(character.ImageUrl, character.Name);
        }
    }
}
