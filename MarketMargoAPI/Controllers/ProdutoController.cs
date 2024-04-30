using MarketMargoAPI.Models;
using MarketMargoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketMargoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController: ControllerBase
    {
        private readonly ConnectionDB _dbContext;

        public ProdutoController(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            ProdutoService produtoService = new ProdutoService(_dbContext);

            IEnumerable<Produto> produtos = await produtoService.GetProdutos();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            ProdutoService produtoService = new ProdutoService(_dbContext);

            Produto? produto = await produtoService.GetProdutoById(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult> CriarProduto(NovoProduto novoProduto)
        {
            try
            {
                ProdutoService produtoService = new ProdutoService(_dbContext);

                Produto produto = new Produto();
                produto.Nome = novoProduto.Nome;
                produto.Setor = novoProduto.Setor;
                produto.Quantidade = novoProduto.Quantidade;
                produto.Data_criacao = DateTime.Now;
                produto.Data_modificacao = DateTime.Now;
                produto.Ativo = true;

                await produtoService.CriarProduto(produto);

                return Ok(produto);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, AtualizarProduto atualizarProduto)
        {
            ProdutoService produtoService = new ProdutoService(_dbContext);

            Produto produto = new Produto();
            produto.Nome = atualizarProduto.Nome;
            produto.Setor = atualizarProduto.Setor;
            produto.Quantidade = atualizarProduto.Quantidade;
            produto.Ativo = atualizarProduto.Ativo;

            var existingProduto = await _dbContext.TbProduto.FindAsync(id);

            if (existingProduto == null)
            {
                return NotFound();
            }

            await produtoService.AtualizarProduto(existingProduto, produto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            ProdutoService produtoService = new ProdutoService(_dbContext);

            var produto = await _dbContext.TbProduto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            await produtoService.DeletarProduto(produto);

            return NoContent();
        }
    }
}
