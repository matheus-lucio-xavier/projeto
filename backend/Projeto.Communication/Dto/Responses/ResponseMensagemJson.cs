using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Communication.Enum;

namespace Projeto.Communication.Dto.Responses
{
    public class ResponseMensagemJson
    {
        public Guid Id { get; set; }
        public Guid OrigemId { get; set; }
        public MensagemTypeEnum Type { get; set; }
        public required string Content { get; set; }
        public bool IsMine { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}