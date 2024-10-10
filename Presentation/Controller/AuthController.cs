using Entities.Dto;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController :ControllerBase
    {
        private readonly IAuthServices _service;

        public AuthController(IAuthServices service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDto register)
        {
            string role = string.IsNullOrEmpty(register.Role) ? "Teacher": register.Role;
            try
            {
                await _service.RegisterUserAsync(register.UserId,
                    register.UserName, 
                    register.Email, 
                    register.Password, 
                    register.Role, 
                    register.Status,
                    register.FirstName,
                    register.LastName);
                var response = new 
                {
                    Success = true,
                    UserName = register.UserName,
                    Email = register.Email,
                    Role = role,
                    Status = register.Status,
                    FirstName = register.FirstName,
                    LastName = register.LastName
                };
               
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Produces("application/json")]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LoginUser(LoginDto login)
        {
            try 
            {
                var token = await _service.LoginUserAsync(login.Email, login.Password);
                return Ok(new {Succes = true, Token = token });
            }
            catch 
            { 
                return Unauthorized();
            }
        }
        [Produces("application/json")]
        [HttpPost("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RefreshToken(TokenDto tokenResponse)
        {
            try 
            {
                var token = await _service.RefreshToken(tokenResponse.RefreshToken);
                return Ok(new {Succes = true, AccessToken = token});
            }
            catch 
            { 
                return Unauthorized();
            }
        }
    }
}
