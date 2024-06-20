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
            PrecoService precoService = new PrecoService(_dbContext);
            CategoriaService categoriaService = new CategoriaService(_dbContext);

            IEnumerable<Produto> produtos = await produtoService.GetProdutos();

            foreach (var item in produtos)
            {
                var preco = precoService.GetPrecoByProdutoId(item.Id).Result;
                item.Preco = preco != null ? preco.Valor.ToString("N2").Replace(".", ",") : string.Empty;
                item.NomeCategoria = categoriaService.GetCategoriaById(item.Id_Categoria).Result.Nome;

            }

            return Ok(produtos);
        }

        [HttpGet("id_categoria")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos(int id_categoria)
        {
            ProdutoService produtoService = new ProdutoService(_dbContext);

            IEnumerable<Produto> produtos = await produtoService.GetProdutosByIdCategoria(id_categoria);

            CategoriaService categoriaService = new CategoriaService(_dbContext);
            PrecoService precoService = new PrecoService(_dbContext);

            foreach (var item in produtos)
            {
                item.NomeCategoria = categoriaService.GetCategoriaById(item.Id_Categoria).Result.Nome;
                item.Preco = precoService.GetPrecoByProdutoId(item.Id).Result.Valor.ToString() ;
            }

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
                produto.Id_Categoria = novoProduto.Id_Categoria;
                produto.Data_criacao = DateTime.Now;
                produto.Data_modificacao = DateTime.Now;
                produto.Ativo = true;

                await produtoService.CriarProduto(produto);

                PrecoService precoService = new PrecoService(_dbContext);
                Preco newPreco = new Preco()
                {
                    IdProduto = produto.Id,
                    Valor = novoProduto.Preco,
                    Ativo = true,
                    Data_criacao = DateTime.Now,
                    Data_modificacao = DateTime.Now,
                    PorcentagemAumento = 0
                };

                await precoService.CriarPreco(newPreco);

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
