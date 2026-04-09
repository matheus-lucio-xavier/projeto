using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Communication.Dto.Requests;
using Projeto.Communication.Dto.Responses;
using Projeto.Domain.Entities;

namespace Projeto.Application.Services.Conversa
{
    public interface IConversaService
    {

        Task<List<ConversaModel>> Consultar();

        // Nullable pois o usuário pode não ser encontrado
        Task<ServiceResponse<ConversaModel>> ConsultarId(Guid id);
        Task<ServiceResponse<List<ResponseMensagemJson>>> ConsultarMensagens(Guid id);
        Task<ServiceResponse<ResponseConversaJson>> Cadastrar(RequestConversaRegisterJson user);
        Task<ServiceResponse<ResponseMembroJson>> AdcionarMembro(Guid conversaId, Guid userId);
        Task<ServiceResponse<ResponseMensagemJson>> adcionarMensagem(Guid conversaId, Guid userId, RequestMensagemRegisterJson mensagem);
        Task<ServiceResponse<ResponseConversaJson>> Deletar(Guid id);
    }
}