using Processo_seletivo_IATec.Dominio;

namespace Processo_seletivo_IATec.Infra.Repositorio.PessoaRepositorio
{
    public interface IPessoaRepositorio
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa> GetByIdAsync(Guid id);
        Task AddAsync(Pessoa pessoa);
        Task UpdateAsync(Pessoa pessoa);
        Task DeleteAsync(Guid id);
        Pessoa? ObterPessoaCpf(string cpf);
    }
}
