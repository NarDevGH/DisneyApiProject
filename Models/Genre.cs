using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Models
{
    public class Genre
    {
        [Key] public int Genre_Id { get; set; }

        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        public List<MovieSerie>? MoviesAndSeries { get; set; }
    }
}
