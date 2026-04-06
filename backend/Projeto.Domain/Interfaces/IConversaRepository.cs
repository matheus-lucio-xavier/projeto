using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface IConversaRepository : IRepository
    {
        Task<ConversaModel?> ConsultarConversaCompleta(Guid id);
        Task<IQueryable<MensagemModel>?> ConsultarMensagens(Guid id);
    }
}