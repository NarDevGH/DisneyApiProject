using System.ComponentModel.DataAnnotations;

namespace DisneyApi.ViewModel.Login
{
    public class LoginRequestViewModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
