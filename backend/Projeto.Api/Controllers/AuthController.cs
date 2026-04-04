using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Services.Auth;
using Projeto.Communication.Dto.Requests;
using Projeto.Communication.Dto.Responses;

namespace Projeto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous] // Login precisa ficar público para emitir o primeiro JWT
        [ProducesResponseType(typeof(ResponseAuthLoginJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] RequestAuthLoginJson auth)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.Login(auth);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("register")]
        [AllowAnonymous] // Registro padrão é público e cria usuário profissional
        [ProducesResponseType(typeof(ResponseAuthRegisterJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RequestAuthRegisterJson request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.Register(request);

            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}