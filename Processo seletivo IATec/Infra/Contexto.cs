using Microsoft.EntityFrameworkCore;
using Processo_seletivo_IATec.Dominio;

namespace Processo_seletivo_IATec.Infra
{
    public class Contexto : DbContext
    {
        //setando tabela de pessoas para pessoa para o contexto
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Telefone> Telefone { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    }
}
