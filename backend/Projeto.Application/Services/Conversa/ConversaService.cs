using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto.Communication.Dto.Requests;
using Projeto.Communication.Dto.Responses;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Application.Services.Conversa
{
    public class ConversaService : IConversaService
    {
        private readonly IConversaRepository _repository;
        private readonly IUnitOfWork _unit;

        public ConversaService(IConversaRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<List<ConversaModel>> Consultar()
        {
            return await _repository.Consultar<ConversaModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        // Nullable pois o usuário pode não ser encontrado
        public async Task<ServiceResponse<ConversaModel>> ConsultarId(Guid id)
        {
            var conversa = await _repository.ConsultarPorId<ConversaModel>(id);

            if (conversa is null)
                return ServiceResponse<ConversaModel>.BadRequest("Essa conversa nao existe");

            return ServiceResponse<ConversaModel>.Ok(conversa);
        }
        public async Task<ServiceResponse<List<ResponseMensagemJson>>> ConsultarMensagens(Guid id)
        {
            var conversa = await _repository.ConsultarPorId<ConversaModel>(id);
            if (conversa is null)
                return ServiceResponse<List<ResponseMensagemJson>>.BadRequest("Essa converca nao existe");

            var query = _repository.ConsultarMensagens(id);

            var mensagens = await query.Select(m => new ResponseMensagemJson
                {
                    Id = m.Id,
                    OrigemId = m.OrigemId,
                    Content = m.Content,
                    CreatedAt = m.CreatedAt
                }).ToListAsync();

            return ServiceResponse<List<ResponseMensagemJson>>.Ok(mensagens);
        }
        public async Task<ServiceResponse<ResponseConversaJson>> Cadastrar(RequestConversaRegisterJson conversa)
        {

            var novo = new ConversaModel
            {
                Nome = conversa.Nome,
                Type = conversa.Type
            };

            await _repository.Cadastrar(novo);
            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseConversaJson>.Ok(new ResponseConversaJson
                { 
                    Id = novo.Id,
                    Type = novo.Type,
                    Nome = novo.Nome
                });

            return ServiceResponse<ResponseConversaJson>.Error("Nao foi possivel cadastrar conversa");
        }
        public async Task<ServiceResponse<ResponseMembroJson>> AdcionarMembro(Guid conversaId, Guid userId)
        {
            var conversa = _repository.ConsultarPorId<ConversaModel>(conversaId);
            var user = _repository.ConsultarPorId<UserModel>(userId);

            if ( conversa is null || user is null)
                return ServiceResponse<ResponseMembroJson>.BadRequest("Converca e/ou Usuario nao existem");
            
            var membro = new MembrosConversaModel
            {
                ConversaId = conversaId,
                UserId = userId,
            };

            await _repository.Cadastrar(membro);
            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseMembroJson>.Ok(new ResponseMembroJson
                {
                    ConversaId = conversaId,
                    UserId = userId
                });
            
            return ServiceResponse<ResponseMembroJson>.Error("Nao foi possivel adcionar membro");
        }
        public async Task<ServiceResponse<ResponseMensagemJson>> adcionarMensagem(Guid conversaId, Guid userId, RequestMensagemRegisterJson mensagem)
        {
            var conversa = _repository.ConsultarPorId<ConversaModel>(conversaId);
            var user = _repository.ConsultarPorId<UserModel>(userId);

            if ( conversa is null || user is null)
                return ServiceResponse<ResponseMensagemJson>.BadRequest("Converca e/ou Usuario nao existem");

            var novaMensagem = new MensagemModel
            {
                OrigemId = userId,
                ConversaId = conversaId,
                Type = mensagem.Type,
                Content = mensagem.Content
            };

            await _repository.Cadastrar(novaMensagem);
            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseMensagemJson>.Ok(new ResponseMensagemJson
                {
                    Id = novaMensagem.Id,
                    OrigemId = novaMensagem.OrigemId,
                    Type = novaMensagem.Type,
                    Content = novaMensagem.Content,
                    CreatedAt = novaMensagem.CreatedAt
                });
            
            return ServiceResponse<ResponseMensagemJson>.Error("Nao foi possivel adcionar membro");
        }
        public async Task<ServiceResponse<ResponseConversaJson>> Deletar(Guid id)
        {
            var existente = await _repository.ConsultarPorId<ConversaModel>(id);
            if (existente is null)
                return ServiceResponse<ResponseConversaJson>.BadRequest("Esse usuario nao existe");

            _repository.Excluir(existente);
            var saved = await _unit.Commit();
            if (saved)
                return ServiceResponse<ResponseConversaJson>.Ok(new ResponseConversaJson
                { 
                    Id = existente.Id,
                    Nome = existente.Nome,
                    Type = existente.Type
                });

            return ServiceResponse<ResponseConversaJson>.Error("Nao foi possivel deletar conversa");
        }

        
    }
}