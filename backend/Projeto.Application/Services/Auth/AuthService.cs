using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Projeto.Communication.Dto.Requests;
using Projeto.Communication.Dto.Responses;
using Projeto.Domain.Interfaces;
using Projeto.Domain.Entities;

namespace Projeto.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthService> _logger;
        private readonly IUnitOfWork _unit;

        // ILogger injetado para registrar erros sem silenciá-los
        public AuthService(IUserRepository repository, TokenService tokenService, ILogger<AuthService> logger, IUnitOfWork unit)
        {
            _repository = repository;
            _tokenService = tokenService;
            _logger = logger;
            _unit = unit;
        }

        public async Task<ServiceResponse<ResponseAuthLoginJson>> Login(RequestAuthLoginJson auth)
        {
            var usuario = await _repository.ConsultarUsuarioPorEmail(auth.Email);

            // Roda o BCrypt em background para não bloquear o fluxo principal
            // Task.Run retorna false se o usuário não existir, evitando exception
            var senhaValida = usuario != null && await Task.Run(() => BCrypt.Net.BCrypt.Verify(auth.Password, usuario.PasswordHash));

            // Mesma mensagem para email inexistente e senha errada (segurança)
            if (usuario == null || !senhaValida)
                return ServiceResponse<ResponseAuthLoginJson>.Unauthorized("Email ou senha inválidos");

            if (!usuario.Active)
                return ServiceResponse<ResponseAuthLoginJson>.Unauthorized("Usuário inativo");

            // gerar o token do JWT
            var token = _tokenService.GenerateToken(usuario);

            return ServiceResponse<ResponseAuthLoginJson>.Ok(new ResponseAuthLoginJson
            {
                Token = token,
                ExpiresIn = "8h",
                Usuario = new ResponseUserJson
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                }
            });
        }

        public async Task<ServiceResponse<ResponseAuthRegisterJson>> Register(RequestAuthRegisterJson request)
        {
            var emailExiste = await _repository.ConsultarUsuarioPorEmail(request.Email);
            if (emailExiste != null)
                return ServiceResponse<ResponseAuthRegisterJson>.BadRequest("Email já cadastrado");

            // BCrypt em background — HashPassword é pesado por design (segurança)
            var passwordHash = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(request.Password));

            var usuario = new UserModel
            {
                
                Email = request.Email,
                Nome = request.Nome,
                PasswordHash = passwordHash,
                Active = true
            };

            try
            {
                await _repository.Cadastrar(usuario);

                await _unit.Commit();
            }
            catch (Exception ex)
            {
                // Loga o erro real para debug, mas retorna mensagem genérica ao cliente
                _logger.LogError(ex, "Erro ao registrar usuário: {Email}", request.Email);
                return ServiceResponse<ResponseAuthRegisterJson>.Error("Não foi possível registrar o usuário");
            }

            // Após o cadastro o EF Core já preencheu usuario.Id com o valor gerado pelo banco
            // Só geramos o token aqui para garantir que o Id é válido
            var token = _tokenService.GenerateToken(usuario);

            return ServiceResponse<ResponseAuthRegisterJson>.Ok(new ResponseAuthRegisterJson
            {
                Message = "Usuário registrado com sucesso",
                Token = token,
                Usuario = new ResponseUserJson
                {
                    Id = usuario.Id,   // Id já populado pelo EF Core
                    Email = usuario.Email
                }
            });
        }
    }
}