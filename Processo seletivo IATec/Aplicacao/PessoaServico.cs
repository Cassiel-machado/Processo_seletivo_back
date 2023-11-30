using Processo_seletivo_IATec.Dominio;
using Processo_seletivo_IATec.Infra.Repositorio.PessoaRepositorio;

namespace Processo_seletivo_IATec.Aplicacao
{
    public class PessoaServico
    {
        //inejecao de depencencia
        private readonly IPessoaRepositorio _pessoaRepositorio;
        //foi utilizado em algumas regras de negocio por isso que foi adicionado
        private readonly TelefoneServico telefoneServico;

        public PessoaServico(IPessoaRepositorio pessoaRepositorio, TelefoneServico telefoneServico)
        {
            _pessoaRepositorio = pessoaRepositorio ?? throw new ArgumentNullException(nameof(pessoaRepositorio));
            this.telefoneServico = telefoneServico;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodasPessoasAsync()
        {
            return await _pessoaRepositorio.GetAllAsync();
        }

        public async Task<Pessoa> ObterPessoaPorIdAsync(Guid id)
        {
            return await _pessoaRepositorio.GetByIdAsync(id);
        }

        public async Task AdicionarPessoaAsync(Pessoa pessoa)
        {
            ValidadorPessoa(pessoa);

            await _pessoaRepositorio.AddAsync(pessoa);
        }

        public async Task AtualizarPessoaAsync(Guid id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                throw new ArgumentException("O ID informado não corresponde ao ID da pessoa.");
            }

            ValidadorPessoa(pessoa);

            await _pessoaRepositorio.UpdateAsync(pessoa);
        }

        public async Task RemoverPessoaAsync(Guid id)
        {

            var telefones = await telefoneServico.ObterTelefonePorPessaoIdAsync(id);

            foreach (var telefone in telefones)
            {
                await telefoneServico.RemoverTelefoneAsync(telefone.Id);
            }

            await _pessoaRepositorio.DeleteAsync(id);
        }

        public Pessoa? ObterPessoaCpf(string cpf)
        {
            return _pessoaRepositorio.ObterPessoaCpf(cpf);
        }

        private void ValidadorPessoa(Pessoa pessoa)
        {
            if (!ValidadarDataNascimento(pessoa.DataNascimento))
                throw new Exception("Data Inválida");

            if (!ValidarCPF(pessoa.Cpf))
                throw new Exception("CPF Inválido");

            if(ValidadarCpfEmUso(pessoa.Cpf))
                throw new Exception("CPF em uso");
        }

        private bool ValidadarDataNascimento(DateTime dataNascimento)
        {
            return dataNascimento < DateTime.Now;
        }

        private bool ValidadarCpfEmUso(string cpf)
        {
            return ObterPessoaCpf(cpf) != null;
        }

        private bool ValidarCPF(string cpf)
        {
            // Remove caracteres não numéricos do CPF
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF possui 11 dígitos e se não é uma sequência repetida
            if (cpf.Length != 11 || VerificarSequenciaRepetida(cpf))
            {
                return false;
            }

            // Cálculo do primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int resto = soma % 11;
            int primeiroDigitoVerificador = (resto < 2) ? 0 : 11 - resto;

            // Verificação do primeiro dígito verificador
            if (int.Parse(cpf[9].ToString()) != primeiroDigitoVerificador)
            {
                return false;
            }

            // Cálculo do segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            resto = soma % 11;
            int segundoDigitoVerificador = (resto < 2) ? 0 : 11 - resto;

            // Verificação do segundo dígito verificador
            return int.Parse(cpf[10].ToString()) == segundoDigitoVerificador;
        }

        private static bool VerificarSequenciaRepetida(string valor)
        {
            //verifica se sao iguais
            for (int i = 1; i < valor.Length; i++)
            {
                if (valor[i] != valor[0])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
