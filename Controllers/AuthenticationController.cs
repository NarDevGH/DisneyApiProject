using DisneyApi.Models;
using DisneyApi.ViewModel.Login;
using DisneyApi.ViewModel.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DisneyApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestViewModel model) 
        {
            // Revisar si existe el usuario:
            //      Si existe, devolver un error.
            //      Si No existe, devolver el usuario.

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists is not null) {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded) { 
                return StatusCode(StatusCodes.Status500InternalServerError, new {
                    Status = "Error",
                    Message = $"User creation failed! ({string.Join(',', result.Errors.Select(x=>x.Description))})."
                });
            }

            return Ok(new {
                Status = "Succes",
                Message = "user created Successfully!"
            });;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestViewModel model) 
        {
            // Tenemos que chequear que el usuario exista.
            // Tenemos que validar que la password ingresada es correcta.
            // Generar Token.
            // Devolver Token Creado.

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user.IsActive)
                {
                    return Ok(await GetToken(user));
                }
                else 
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        status = "Error",
                        message = "El Usuario no esta autorizado."
                    });
                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized, new {
                status = "Error",
                message = "Constrasenia incorrecta o usuario inexistente."
            });
        }

        private async Task<LoginResponseViewModel> GetToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Claim para indicar que es un token jwt.
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authsigninkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTRefreshTokenHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7158", 
                audience: "https://localhost:7158", 
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authsigninkey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
