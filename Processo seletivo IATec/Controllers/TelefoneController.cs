using Microsoft.AspNetCore.Mvc;
using Processo_seletivo_IATec.Aplicacao;
using Processo_seletivo_IATec.Dominio;

namespace Processo_seletivo_IATec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        private readonly TelefoneServico _telefoneServico;

        public TelefoneController(TelefoneServico telefoneServico)
        {
            _telefoneServico = telefoneServico ?? throw new ArgumentNullException(nameof(telefoneServico));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefone>>> Get()
        {
            var telefone = await _telefoneServico.ObterTodosTelefonesAsync();
            return Ok(telefone);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Telefone>> GetById(Guid id)
        {
            var telefone = await _telefoneServico.ObterTelefonePorIdAsync(id);
            if (telefone == null)
            {
                return NotFound();
            }

            return Ok(telefone);
        }

        [HttpGet("Pessoa/{id}")]
        public async Task<ActionResult<Telefone>> GetByIdPessoa(Guid id)
        {
            var telefone = await _telefoneServico.ObterTelefonePorPessaoIdAsync(id);
            if (telefone == null)
            {
                return NotFound();
            }

            return Ok(telefone);
        }

        [HttpPost]
        public async Task<ActionResult<Telefone>> Create(Telefone telefone)
        {
            await _telefoneServico.AdicionarTelefoneAsync(telefone);
            return CreatedAtAction(nameof(GetById), new { id = telefone.Id }, telefone);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _telefoneServico.RemoverTelefoneAsync(id);
            return NoContent();
        }
    }
}
