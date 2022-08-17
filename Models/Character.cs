using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Models
{
    public class Character
    {
        [Key] public int Character_Id { get; set; }

        public string? ImageUrl { get; set; }

        [Required] public string Name { get; set; }

        [Range(0,int.MaxValue)] public int? Age { get; set; }

        [Range(0,float.MaxValue)] public float? Weight { get; set; }

        public string? History { get; set; }

        public ICollection<MovieSerie>? MoviesAndSeries { get; set; }
    }
}
