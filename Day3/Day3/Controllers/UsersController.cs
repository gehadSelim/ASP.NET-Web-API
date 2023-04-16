using Day3.Data;
using Day3.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Day3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("RegisterAd")]
        public async Task<ActionResult> RegisterAdmin(RegisterDto registerDto)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Department = registerDto.Department,
            };

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim(ClaimTypes.Role, "admin"),
        };
            await _userManager.AddClaimsAsync(appUser, claims);

            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Department = registerDto.Department,
            };

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim(ClaimTypes.Role, "user"),
        };
            await _userManager.AddClaimsAsync(appUser, claims);

            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var isAuthenticated = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            var claimsList = await _userManager.GetClaimsAsync(user);

            var secretKeyString = _configuration.GetValue<string>("SecretKey");
            var secretyKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString ?? string.Empty);
            var secretKey = new SymmetricSecurityKey(secretyKeyInBytes);

            var signingCredentials = new SigningCredentials(secretKey,
                SecurityAlgorithms.HmacSha256Signature);

            var expireDate = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                claims: claimsList,
                expires: expireDate,
                signingCredentials: signingCredentials);

            var tokenHndler = new JwtSecurityTokenHandler();
            var tokenString = tokenHndler.WriteToken(token);

            return new TokenDto(tokenString, expireDate);
        }

    }
}
