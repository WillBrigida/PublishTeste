using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EstudoIdentity.Shared.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace EstudoIdentity.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly SignInManager<IdentityUser> _signInManager;
        readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration ,SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Credencial inválida." });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Email)
            };

            var appSettinsSection = _configuration.GetSection("AppSettings");
            var appSettins = appSettinsSection.Get<AppSettings>();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettins.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddHours(appSettins.ExpiracaoHoras);

            var token = new JwtSecurityToken(
                appSettins.Emissor,
                appSettins.ValidoEm,
                claims,
                expires: expiry,
                signingCredentials: creds
                );

            return Ok(new LoginResult {Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
