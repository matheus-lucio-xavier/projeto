using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Domain.Entities;

namespace Projeto.Domain.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<UserModel?> ConsultarUsuarioPorEmail(string email);
        Task<UserModel?> ConsultarUsuarioCompleto(Guid id);
    }
}