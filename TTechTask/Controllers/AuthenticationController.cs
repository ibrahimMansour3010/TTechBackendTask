using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTechTash.Domain.DTOs;
using TTechTash.Domain.Services.Abstraction;
using TTechTask.Domain.DTOs;

namespace TTechTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO.Register model)
        {
            try
            {
                return Ok(await _authenticationService.Register(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDTO.Login model)
        {
            try
            {
                return Ok(await _authenticationService.Login(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message });
            }
        }
    }
}
