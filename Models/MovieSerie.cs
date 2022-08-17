using System.ComponentModel.DataAnnotations;

namespace DisneyApi.Models
{
    public class MovieSerie
    {
        [Key] public int MovieSerie_Id { get; set; }

        public string? ImageUrl { get; set; }

        [Required] public string Title { get; set; }

        public DateTime? CreationDate { get; set; }

        [Range(1,5)] public int? Rate { get; set; }

        public ICollection<Character>? Characters { get; set; }
    }
}
