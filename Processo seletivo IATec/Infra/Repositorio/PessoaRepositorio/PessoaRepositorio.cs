using Microsoft.EntityFrameworkCore;
using Processo_seletivo_IATec.Dominio;

namespace Processo_seletivo_IATec.Infra.Repositorio.PessoaRepositorio
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly Contexto _contexto;

        public PessoaRepositorio(Contexto contexto)
        {
            _contexto = contexto;
            //usado para manipular os dados da tabela Pessoa
        }

        //metodos diversos
        //adiciona pessoa
        public async Task AddAsync(Pessoa pessoa)
        {
            _contexto.Pessoa.Add(pessoa);
            await _contexto.SaveChangesAsync();
        }

        //deleta
        public async Task DeleteAsync(Guid id)
        {
            var pessoa = await GetByIdAsync(id);
            if (pessoa != null)
            {
                _contexto.Pessoa.Remove(pessoa);
                await _contexto.SaveChangesAsync();
            }
        }

        //pega todas as pessoas
        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _contexto.Pessoa.ToListAsync();
        }

        //pega pelo id
        public async Task<Pessoa> GetByIdAsync(Guid id)
        {
            return await _contexto.Pessoa.FindAsync(id);
        }

        //atualiza a pessoa
        public async Task UpdateAsync(Pessoa pessoa)
        {
            //recebe um pessoa e coloca o estado dele como modificado e logo apos ja salva
            _contexto.Entry(pessoa).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        //pega o cpf da pessoa
        //pode ser ou pessoa ou nulo
        public Pessoa? ObterPessoaCpf(string cpf)
        {
            return _contexto.Pessoa.FirstOrDefault(p => p.Cpf == cpf);
        }
    }

}
