using Processo_seletivo_IATec.Dominio;
using Processo_seletivo_IATec.Infra.Repositorio.PessoaRepositorio;
using Processo_seletivo_IATec.Infra.Repositorio.TelefoneRepositorio;

namespace Processo_seletivo_IATec.Aplicacao
{
    public class TelefoneServico
    {
        private readonly ITelefoneRepositorio _telefoneRepositorio;
        
        public TelefoneServico(ITelefoneRepositorio telefoneRepositorio)
        {
            _telefoneRepositorio = telefoneRepositorio ?? throw new ArgumentNullException(nameof(telefoneRepositorio));
        }
        public async Task<IEnumerable<Telefone>> ObterTodosTelefonesAsync()
        {
            return await _telefoneRepositorio.GetAllAsync();
        }

        public async Task<Telefone> ObterTelefonePorIdAsync(Guid id)
        {
            return await _telefoneRepositorio.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Telefone>> ObterTelefonePorPessaoIdAsync(Guid id)
        {
            return await _telefoneRepositorio.GetByPessoaId(id);
        }

        public async Task AdicionarTelefoneAsync(Telefone telefone)
        {
            ValidadorTelefone(telefone);

            await _telefoneRepositorio.AddAsync(telefone);
        }

        public async Task RemoverTelefoneAsync(Guid id)
        {
            await _telefoneRepositorio.DeleteAsync(id);
        }

        private void ValidadorTelefone(Telefone telefone)
        {
            if (!ValidadorNumero(telefone.NumeroTelefone))
                throw new Exception("Número Inválido");
        } 

        private bool ValidadorNumero(string numero)
        {
            // Remove caracteres não numéricos do telefone
            numero = new string(numero.Where(char.IsDigit).ToArray());

            return numero.Length == 10;
        }
    }
}
