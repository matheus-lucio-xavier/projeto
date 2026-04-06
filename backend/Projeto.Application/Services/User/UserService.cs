using Projeto.Communication.Dto.Requests;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Projeto.Communication.Dto.Responses;

namespace Projeto.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unit;

        public UserService(IUserRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<List<UserModel>> Consultar()
        {
            return await _repository.Consultar<UserModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<ServiceResponse<UserModel>> ConsultarId(Guid id)
        {
            var user = await _repository.ConsultarPorId<UserModel>(id);

            if (user is null)
                return ServiceResponse<UserModel>.BadRequest("Esse usuario nao existe");

            return ServiceResponse<UserModel>.Ok(user);
        }

        public async Task<ServiceResponse<ResponseUserJson>> Cadastrar(RequestUserRegisterJson user)
        {
            var emailExiste = await _repository.ConsultarUsuarioPorEmail(user.Email);
            if (emailExiste != null)
                return ServiceResponse<ResponseUserJson>.BadRequest("Email já cadastrado");

            // Mapeia DTO para entidade — hash da senha deve ser feito aqui antes de salvar
            var novo = new UserModel
            {
                Nome = user.Nome,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Active = user.Active
            };

            await _repository.Cadastrar(novo);
            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseUserJson>.Ok(new ResponseUserJson
                { 
                    Id = novo.Id,
                    Nome = novo.Nome,
                    Email = novo.Email
                });

            return ServiceResponse<ResponseUserJson>.Error("Nao foi possivel cadastrar usuario");
        }

        public async Task<ServiceResponse<ResponseUserJson>> Editar(RequestUserRegisterJson user)
        {

            var existente = await _repository.ConsultarPorId<UserModel>(user.Id);
            if (existente is null)
                return ServiceResponse<ResponseUserJson>.BadRequest("Esse usuario nao existe");
                
            var emailExiste = await _repository.ConsultarUsuarioPorEmail(user.Email);
            if (emailExiste != null)
                return ServiceResponse<ResponseUserJson>.BadRequest("Email já cadastrado");


            var novo = new UserModel
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Active = user.Active
            };

            await _repository.Editar(novo);
            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseUserJson>.Ok(new ResponseUserJson
                { 
                    Id = novo.Id,
                    Nome = novo.Nome,
                    Email = novo.Email
                });

            return ServiceResponse<ResponseUserJson>.Error("Nao foi possivel editar usuario");
        }

        public async Task<ServiceResponse<ResponseUserJson>> Deletar(Guid id)
        {
            var existente = await _repository.ConsultarPorId<UserModel>(id);
            if (existente is null)
                return ServiceResponse<ResponseUserJson>.BadRequest("Esse usuario nao existe");

            _repository.Excluir(existente);
            var saved = await _unit.Commit();
            if (saved)
                return ServiceResponse<ResponseUserJson>.Ok(new ResponseUserJson
                { 
                    Id = existente.Id,
                    Nome = existente.Nome,
                    Email = existente.Email
                });

            return ServiceResponse<ResponseUserJson>.Error("Nao foi possivel deletar usuario");
        }
    }
}