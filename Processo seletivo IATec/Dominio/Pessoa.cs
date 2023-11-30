namespace Processo_seletivo_IATec.Dominio
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
    }
}
