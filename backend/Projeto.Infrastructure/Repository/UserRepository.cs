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
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<UserModel?> ConsultarUsuarioPorEmail(string email)
        {
            return await _appDbContext.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.Email == email);
        }

        public async Task<UserModel?> ConsultarUsuarioCompleto(Guid id)
        {
            return await _appDbContext.Usuarios
            .Include(u => u.Conversas)
                .ThenInclude(c => c.Conversa)
            .Include(c => c.Mensagens)
            .FirstOrDefaultAsync(c => c.Id == id);

        }
        
    }
}