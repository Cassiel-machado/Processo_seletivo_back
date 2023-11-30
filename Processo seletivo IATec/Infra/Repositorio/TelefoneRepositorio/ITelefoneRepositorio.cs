using Processo_seletivo_IATec.Dominio;

namespace Processo_seletivo_IATec.Infra.Repositorio.TelefoneRepositorio
{
    public interface ITelefoneRepositorio
    {
        Task<IEnumerable<Telefone>> GetAllAsync();
        Task<Telefone> GetByIdAsync(Guid id);
        Task AddAsync(Telefone telefone);
        Task UpdateAsync(Telefone telefone);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Telefone>> GetByPessoaId(Guid idPessoa);
    }
}
