using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Communication.Enum;

namespace Projeto.Domain.Entities
{
    public class MensagemModel
    {
        public Guid Id { get; set; }

        public Guid OrigemId { get; set; }
        public required UserModel Origem { get; set; }
        
        public Guid ConversaId { get; set; }
        public required ConversaModel Conversa { get; set; }
        public MensagemTypeEnum Type { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? EditedAt { get; set; } 

    }
}