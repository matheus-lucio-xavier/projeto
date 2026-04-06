using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Services.User;
using Projeto.Communication.Dto.Requests;

namespace Projeto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        // Interface em vez da classe concreta
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            var response = await _service.Consultar();
            return Ok(response);
        }

        [HttpGet("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserId(Guid id)
        {
            var response = await _service.ConsultarId(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUser([FromBody] RequestUserRegisterJson user)
        // RequestUserRegisterJson em vez de UserModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Cadastrar(user);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUser([FromBody] RequestUserRegisterJson user)
        // RequestUserRegisterJson em vez de UserModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Editar(user);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _service.Deletar(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}