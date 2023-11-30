using Microsoft.AspNetCore.Mvc;
using Processo_seletivo_IATec.Aplicacao;
using Processo_seletivo_IATec.Dominio;

namespace Processo_seletivo_IATec.Controllers
{   //localhost/api/pessoa
    //camada que recebe requisicao
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaServico _pessoaServico;
        //injecao de dependencia
        public PessoaController(PessoaServico pessoaServico)
        {
            _pessoaServico = pessoaServico ?? throw new ArgumentNullException(nameof(pessoaServico));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
            var pessoas = await _pessoaServico.ObterTodasPessoasAsync();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetById(Guid id)
        {
            var pessoa = await _pessoaServico.ObterPessoaPorIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> Create(Pessoa pessoa)
        {
            await _pessoaServico.AdicionarPessoaAsync(pessoa);
            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Pessoa pessoa)
        {
            try
            {
                await _pessoaServico.AtualizarPessoaAsync(id, pessoa);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("O ID informado não corresponde ao ID da pessoa.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _pessoaServico.RemoverPessoaAsync(id);
            return NoContent();
        }
    }


}
