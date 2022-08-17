using System.ComponentModel.DataAnnotations;

namespace DisneyApi.ViewModel.Register
{
    public class RegisterRequestViewModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
