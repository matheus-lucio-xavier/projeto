using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Entities;

namespace Projeto.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options){}
        
           
        
        public DbSet<UserModel> Usuarios { get; set; }
        public DbSet<ConversaModel> Conversas { get; set; }
        public DbSet<MensagemModel> Mensagens { get; set; }
        public DbSet<MembrosConversaModel> MembrosConversas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fks de MensagemModel
            modelBuilder.Entity<MensagemModel>()
                .HasOne(m => m.Origem)
                .WithMany(u => u.Mensagens)
                .HasForeignKey(m => m.OrigemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MensagemModel>()
                .HasOne(m => m.Conversa)
                .WithMany(c => c.Mensagens)
                .HasForeignKey(m => m.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);

            // fks de MembrosConversaModel
            modelBuilder.Entity<MembrosConversaModel>()
                .HasOne(m => m.User)
                .WithMany(u => u.Conversas)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MembrosConversaModel>()
                .HasOne(m => m.Conversa)
                .WithMany(c => c.Membros)
                .HasForeignKey(m => m.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);

            // impede repetição de membros dentro de uma conversa
            modelBuilder.Entity<MembrosConversaModel>()
                .HasIndex(m => new { m.ConversaId, m.UserId })
                .IsUnique();
        }
    }
}