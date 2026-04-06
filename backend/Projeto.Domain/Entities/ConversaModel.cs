using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Communication.Enum;

namespace Projeto.Domain.Entities
{
    public class ConversaModel
    {
        public Guid Id { get; set; }
        public ConversaTypeEnum Type { get; set; }	
        public required string Nome { get; set; }
        public ICollection<MembrosConversaModel> Membros { get; set; } = new List<MembrosConversaModel>();
        public ICollection<MensagemModel> Mensagens { get; set; } = new List<MensagemModel>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;	
    }
}