using EstudoIdentity.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoIdentity.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        readonly UserManager<IdentityUser> _userManager;
        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Register register)
        {
            var registrarUsuario = new IdentityUser { UserName = register.Email, Email = register.Email };
            var result = await _userManager.CreateAsync(registrarUsuario, register.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return Ok(new RegisterResult {Errors = errors, Successful = false } );
            }

            return Ok(new RegisterResult { Successful = true });
        }

    }
}
