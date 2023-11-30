namespace Processo_seletivo_IATec.Dominio
{
    public class Telefone
    {
        public Guid Id { get; set; }
        public string NumeroTelefone { get; set; }
        public Guid IdPessoa { get; set; }
    }
}
