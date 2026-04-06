using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Communication.Dto.Responses;
using Projeto.Domain.Entities;

namespace Projeto.Application.Services.Conversa
{
    public interface IConversaService
    {

        Task<List<UserModel>> Consultar();

        // Nullable pois o usuário pode não ser encontrado
        Task<ServiceResponse<UserModel>> ConsultarId(Guid id);

        Task<ServiceResponse<ResponseUserJson>> Cadastrar(RequestUserRegisterJson user);
        Task<ServiceResponse<ResponseUserJson>> Editar(RequestUserRegisterJson user);

        // Nullable pois retorna null se o usuário não existir
        Task<ServiceResponse<ResponseUserJson>> Deletar(Guid id);
        Task<ConversaModel?> ConsultarConversaCompleta(Guid id);
        Task<IQueryable<MensagemModel>?> ConsultarMensagens(Guid id);
    }
}