using Microsoft.EntityFrameworkCore;
using Processo_seletivo_IATec.Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Processo_seletivo_IATec.Infra.Repositorio.TelefoneRepositorio
{
    public class TelefoneRepositorio : ITelefoneRepositorio
    {
        private readonly Contexto _contexto;
        //usado para manipular os dados da tabela Pessoa
        public TelefoneRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        //adicona de forma assincrona
        public async Task AddAsync(Telefone telefone)
        {
            _contexto.Telefone.Add(telefone);
            await _contexto.SaveChangesAsync();
        }

        //deleta de forma assincrona
        public async Task DeleteAsync(Guid id)
        {
            var telefone = await GetByIdAsync(id);
            if (telefone != null)
            {
                _contexto.Telefone.Remove(telefone);
                await _contexto.SaveChangesAsync();
            }
        }

        //pega todos
        public async Task<IEnumerable<Telefone>> GetAllAsync()
        {
            return await _contexto.Telefone.ToListAsync();
        }

        //pega pelo id
        public async Task<Telefone> GetByIdAsync(Guid id)
        {
            return await _contexto.Telefone.FindAsync(id);
        }

        //faz um update
        public async Task UpdateAsync(Telefone telefone)
        {
            //recebe um telefone e coloca o estado dele como modificado e logo apos ja salva
            _contexto.Entry(telefone).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        }

        //pega pelo id da pessoa
        public async Task<IEnumerable<Telefone>> GetByPessoaId(Guid idPessoa)
        {
            return await _contexto.Telefone.Where(t => t.IdPessoa == idPessoa).ToListAsync();
        }
    }
}
