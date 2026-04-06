using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Domain.Entities
{
    public class MembrosConversaModel
    {
        public Guid Id { get; set; }
	
        public Guid ConversaId { get; set; }
        public required ConversaModel Conversa { get; set; }
        public Guid UserId { get; set; }
        public required UserModel User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

    }
}