using MarketMargoAPI.Models;
using MarketMargoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketMargoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrecoController: ControllerBase
    {
        private readonly ConnectionDB _dbContext;

        public PrecoController(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("produto_id")]
        public async Task<ActionResult<IEnumerable<Preco>>> GetPrecos(int produto_id)
        {
            PrecoService precoService = new PrecoService(_dbContext);

            IEnumerable<Preco> precos = await precoService.GetPrecos(produto_id);

            return Ok(precos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Preco>> GetPreco(int id)
        {
            PrecoService precoService = new PrecoService(_dbContext);

            Preco? preco = await precoService.GetPrecoById(id);

            if (preco == null)
            {
                return NotFound();
            }

            return preco;
        }

        [HttpPost]
        public async Task<ActionResult> CriarPreco(NovoPreco novoPreco)
        {
            try
            {
                PrecoService precoService = new PrecoService(_dbContext);

                Preco preco = new Preco();
                preco.IdProduto = novoPreco.IdProduto;
                preco.PorcentagemAumento = novoPreco.PorcentagemAumento;
                preco.Valor = novoPreco.Valor;
                preco.Data_criacao = DateTime.Now;
                preco.Data_modificacao = DateTime.Now;
                preco.Ativo = true;

                await precoService.CriarPreco(preco);

                return Ok(preco);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreco(int id, AtualizarPreco atualizarPreco)
        {
            PrecoService precoService = new PrecoService(_dbContext);

            Preco preco = new Preco();
            preco.IdProduto = atualizarPreco.IdProduto;
            preco.PorcentagemAumento = atualizarPreco.PorcentagemAumento;
            preco.Valor = atualizarPreco.Valor;
            preco.Ativo = atualizarPreco.Ativo;

            var existingPreco = await _dbContext.TbPreco.FindAsync(id);

            if (existingPreco == null)
            {
                return NotFound();
            }

            await precoService.AtualizarPreco(existingPreco, preco);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreco(int id)
        {
            PrecoService precoService = new PrecoService(_dbContext);

            var preco = await _dbContext.TbPreco.FindAsync(id);

            if (preco == null)
            {
                return NotFound();
            }

            await precoService.DeletarPreco(preco);

            return NoContent();
        }
    }
}
