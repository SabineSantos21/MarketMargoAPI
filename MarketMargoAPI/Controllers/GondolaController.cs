using MarketMargoAPI.Models;
using MarketMargoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketMargoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GondolaController: ControllerBase
    {
        private readonly ConnectionDB _dbContext;

        public GondolaController(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gondola>>> GetGondolas()
        {
            GondolaService gondolaService = new GondolaService(_dbContext);

            IEnumerable<Gondola> gondolas = await gondolaService.GetGondolas();

            CategoriaService categoriaService = new CategoriaService(_dbContext);

            foreach (var item in gondolas)
            {
                item.NomeCategoria = categoriaService.GetCategoriaById(item.Id_Categoria).Result.Nome;
            }

            return Ok(gondolas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gondola>> GetGondola(int id)
        {
            GondolaService gondolaService = new GondolaService(_dbContext);

            Gondola? gondola = await gondolaService.GetGondolaById(id);

            if (gondola == null)
            {
                return NotFound();
            }

            return gondola;
        }

        [HttpPost]
        public async Task<ActionResult> CriarGondola(NovaGondola novaGondola)
        {
            try
            {
                GondolaService gondolaService = new GondolaService(_dbContext);

                Gondola gondola = new Gondola();
                gondola.Nome = novaGondola.Nome;
                gondola.Capacidade = novaGondola.Capacidade;
                gondola.Id_Categoria = novaGondola.Id_Categoria;
                gondola.Data_criacao = DateTime.Now;
                gondola.Data_modificacao = DateTime.Now;
                gondola.Ativo = true;

                await gondolaService.CriarGondola(gondola);

                return Ok(gondola);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGondola(int id, AtualizarGondola atualizarGondola)
        {
            GondolaService gondolaService = new GondolaService(_dbContext);

            Gondola gondola = new Gondola();
            gondola.Nome = atualizarGondola.Nome;
            gondola.Capacidade = atualizarGondola.Capacidade;
            gondola.Id_Categoria = atualizarGondola.Id_Categoria;
            gondola.Ativo = atualizarGondola.Ativo;

            var existingGondola = await _dbContext.TbGondola.FindAsync(id);

            if (existingGondola == null)
            {
                return NotFound();
            }

            await gondolaService.AtualizarGondola(existingGondola, gondola);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGondola(int id)
        {
            GondolaService gondolaService = new GondolaService(_dbContext);

            var gondola = await _dbContext.TbGondola.FindAsync(id);

            if (gondola == null)
            {
                return NotFound();
            }

            await gondolaService.DeletarGondola(gondola);

            return NoContent();
        }
    }
}
