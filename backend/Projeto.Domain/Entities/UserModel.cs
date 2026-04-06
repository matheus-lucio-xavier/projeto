namespace Projeto.Domain.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public ICollection<MembrosConversaModel> Conversas { get; set; } = new List<MembrosConversaModel>();
	    public ICollection<MensagemModel> Mensagens { get; set; } = new List<MensagemModel>();
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}