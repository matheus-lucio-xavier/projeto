using Microsoft.EntityFrameworkCore;
using Projeto.Communication.Dto.Requests;
using Projeto.Communication.Dto.Responses;
using Projeto.Communication.Enum;
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
        public async Task<ServiceResponse<List<ResponseMensagemJson>>> ConsultarMensagens(Guid userLogadoId, Guid id)
        {
            var conversa = await _repository.ConsultarPorId<ConversaModel>(id);
            if (conversa is null)
                return ServiceResponse<List<ResponseMensagemJson>>.BadRequest("Essa conversa nao existe");

            var query = _repository.ConsultarMensagens(id);

            var membros = _repository.ConsultarMembros(id);
            if (!await membros.AnyAsync(u => u.Id == userLogadoId))
                return ServiceResponse<List<ResponseMensagemJson>>.BadRequest("Apenas membros da conversa podem ver mensagens");

            var mensagens = await query.Select(m => new ResponseMensagemJson
                {
                    Id = m.Id,
                    OrigemId = m.OrigemId,
                    Content = m.Content,
                    IsMine = userLogadoId == m.OrigemId,
                    CreatedAt = m.CreatedAt
                }).ToListAsync();

            return ServiceResponse<List<ResponseMensagemJson>>.Ok(mensagens);
        }

        public async Task<ServiceResponse<List<ResponseUserJson>>> ConsultarMembros(Guid userLogadoId, Guid id)
        {
            var conversa = await _repository.ConsultarPorId<ConversaModel>(id);

            if (conversa is null)
                return ServiceResponse<List<ResponseUserJson>>.BadRequest("Essa conversa nao existe");

            var query = _repository.ConsultarMembros(id);

            if (!await query.AnyAsync(u => u.Id == userLogadoId))
                return ServiceResponse<List<ResponseUserJson>>.BadRequest("Apenas membros da conversa podem ver outros membros");

            var users = await query.Select(m => new ResponseUserJson
                {
                    Id = m.Id,
                    Nome = m.Nome,
                    Email = m.Email
                }).ToListAsync();

            return ServiceResponse<List<ResponseUserJson>>.Ok(users);
        }
        public async Task<ServiceResponse<ResponseConversaJson>> CadastrarPrivado(Guid userLogadoId, Guid userId, RequestConversaRegisterJson conversa)
        {
            var logado = await _repository.ConsultarPorId<UserModel>(userLogadoId);
            var user = await _repository.ConsultarPorId<UserModel>(userId);

            if (user is null)
                return ServiceResponse<ResponseConversaJson>.BadRequest("Usuario com quem esta tentando criar uma conversa nao existe");
            if (logado is null)
                return ServiceResponse<ResponseConversaJson>.BadRequest("É necessario estar logado como um usuario para criar uma conversa");

            var novaConversa = new ConversaModel
            {
                Nome = conversa.Nome,
                Type = ConversaTypeEnum.Privado
            };

            await _repository.Cadastrar(novaConversa);

            var membro1 = new MembrosConversaModel
            {
                ConversaId = novaConversa.Id,
                UserId = userLogadoId,
            };
            var membro2 = new MembrosConversaModel
            {
                ConversaId = novaConversa.Id,
                UserId = userId,
            };

            await _repository.Cadastrar(membro1);
            await _repository.Cadastrar(membro2);

            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseConversaJson>.Ok(new ResponseConversaJson
                { 
                    Id = novaConversa.Id,
                    Type = novaConversa.Type,
                    Nome = novaConversa.Nome
                });

            return ServiceResponse<ResponseConversaJson>.Error("Nao foi possivel cadastrar conversa");
        }

        public async Task<ServiceResponse<ResponseConversaJson>> CadastrarGrupo(Guid userLogadoId, RequestConversaRegisterJson conversa)
        {
            var logado = await _repository.ConsultarPorId<UserModel>(userLogadoId);
            if (logado is null)
                return ServiceResponse<ResponseConversaJson>.BadRequest("É necessario estar logado como um usuario para criar uma conversa");

            var novaConversa = new ConversaModel
            {
                Nome = conversa.Nome,
                Type = ConversaTypeEnum.Grupo
            };

            await _repository.Cadastrar(novaConversa);

            var membro1 = new MembrosConversaModel
            {
                ConversaId = novaConversa.Id,
                UserId = userLogadoId,
            };

            await _repository.Cadastrar(membro1);

            var saved = await _unit.Commit();

            if (saved)
                return ServiceResponse<ResponseConversaJson>.Ok(new ResponseConversaJson
                { 
                    Id = novaConversa.Id,
                    Type = novaConversa.Type,
                    Nome = novaConversa.Nome
                });

            return ServiceResponse<ResponseConversaJson>.Error("Nao foi possivel cadastrar conversa");
        }
        public async Task<ServiceResponse<ResponseMembroJson>> AdcionarMembro(Guid userLogadoId, Guid conversaId, Guid userId)
        {
            var conversa = await _repository.ConsultarPorId<ConversaModel>(conversaId);
            var user = await _repository.ConsultarPorId<UserModel>(userId);

            if ( conversa is null || user is null)
                return ServiceResponse<ResponseMembroJson>.BadRequest("conversa e/ou Usuario nao existem");
            if (conversa.Type != ConversaTypeEnum.Grupo)
                return ServiceResponse<ResponseMembroJson>.BadRequest("Conversa nao e um grupo para ter mais de 2 membros");

            var membros = _repository.ConsultarMembros(conversaId);
            if (!await membros.AnyAsync(u => u.Id == userLogadoId))
                return ServiceResponse<ResponseMembroJson>.BadRequest("Apenas membros da conversa podem adcionar outros membros");
            
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
        public async Task<ServiceResponse<ResponseMensagemJson>> adcionarMensagem(Guid conversaId, Guid userLogadoId, RequestMensagemRegisterJson mensagem)
        {
            var conversa = await _repository.ConsultarPorId<ConversaModel>(conversaId);

            if ( conversa is null)
                return ServiceResponse<ResponseMensagemJson>.BadRequest("conversa nao existem");
            
            var membros = _repository.ConsultarMembros(conversaId);
            if (!await membros.AnyAsync(u => u.Id == userLogadoId))
                return ServiceResponse<ResponseMensagemJson>.BadRequest("Apenas membros da conversa podem enviar mensagens");

            var novaMensagem = new MensagemModel
            {
                OrigemId = userLogadoId,
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
                    IsMine = userLogadoId == novaMensagem.OrigemId,
                    CreatedAt = novaMensagem.CreatedAt
                });
            
            return ServiceResponse<ResponseMensagemJson>.Error("Nao foi possivel adcionar mensagem");
        }
        public async Task<ServiceResponse<ResponseConversaJson>> Deletar(Guid id)
        {
            var existente = await _repository.ConsultarPorId<ConversaModel>(id);
            if (existente is null)
                return ServiceResponse<ResponseConversaJson>.BadRequest("Essa conversa nao existe");

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