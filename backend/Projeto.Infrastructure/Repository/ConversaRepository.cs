using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using Projeto.Infrastructure.Data;

namespace Projeto.Infrastructure.Repository
{
    public class ConversaRepository : Repository, IConversaRepository
    {
        public ConversaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<ConversaModel?> ConsultarConversaCompleta(Guid id)
        {
            return await _appDbContext.Conversas
            .Include(u => u.Membros)
                .ThenInclude(c => c.User)
            .Include(c => c.Mensagens)
            .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IQueryable<MensagemModel>?> ConsultarMensagens(Guid id)
        {
            return _appDbContext.Mensagens
                .Where(m => m.ConversaId == id)
                .OrderByDescending(m => m.CreatedAt);

        }
    }
}