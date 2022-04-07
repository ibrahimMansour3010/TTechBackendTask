using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTechTash.Domain.DTOs;
using TTechTask.Domain.DTOs;

namespace TTechTash.Domain.Services.Abstraction
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<UserDTO.Get>> Register(UserDTO.Register model);
        Task<ApiResponse<UserDTO.LoginResult>> Login(UserDTO.Login model);
    }
}
