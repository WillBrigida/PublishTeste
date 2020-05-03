using EstudoIdentity.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoIdentity.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(Login loginModel);
        Task Logout();
        Task<RegisterResult> Register(Register registerModel);
    }
}
