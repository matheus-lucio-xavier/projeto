using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Services.Conversa;
using Projeto.Communication.Dto.Requests;

namespace Projeto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConversaController : ControllerBase
    {
        private readonly IConversaService _service;

        public ConversaController(IConversaService service)
        {
            _service = service;
        }

        [HttpGet("conversas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConversa()
        {
            var response = await _service.Consultar();
            return Ok(response);
        }

        [HttpGet("conversas/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConversaId(Guid id)
        {
            var response = await _service.ConsultarId(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("conversas/{id}/mensagens")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetConversaMensagens(Guid id)
        {
            var response = await _service.ConsultarMensagens(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("conversas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostConversa([FromBody] RequestConversaRegisterJson conversa)
        // RequestConversaRegisterJson em vez de conversaModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Cadastrar(conversa);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("conversas/{conversaId}/adcinar-membro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdcionarMembro(Guid conversaId, Guid userId)
        // RequestConversaRegisterJson em vez de conversaModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.AdcionarMembro(conversaId, userId);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("conversas/{conversaId}/adcinar-mensagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdcionarMensagem(Guid conversaId, RequestMensagemRegisterJson mensagem)
        // RequestConversaRegisterJson em vez de conversaModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();

            var response = await _service.adcionarMensagem(conversaId, userId, mensagem);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("conversas/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deleteconversa(Guid id)
        {
            var response = await _service.Deletar(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        private Guid GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        if (!Guid.TryParse(userId, out var id))
            throw new Exception("Id do usuário inválido no token");

        return id;
    }
    }
}