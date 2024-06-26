using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class ProdutoService
    {
        private readonly ConnectionDB _dbContext;

        public ProdutoService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _dbContext.TbProduto.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosByIdCategoria(int id_categoria)
        {
            return await _dbContext.TbProduto.Where(p => p.Id_Categoria == id_categoria).ToListAsync();
        }

        public async Task<Produto?> GetProdutoById(int id)
        {
            return await _dbContext.TbProduto.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> CriarProduto(Produto produto)
        {
            _dbContext.TbProduto.Add(produto);
            await _dbContext.SaveChangesAsync();

            return produto;
        }

        public async Task AtualizarProduto(Produto existingProduto, Produto produto)
        {
            existingProduto.Nome = produto.Nome;
            existingProduto.Setor = produto.Setor;
            existingProduto.Quantidade = produto.Quantidade;
            existingProduto.Ativo = produto.Ativo;
            existingProduto.Data_modificacao = DateTime.Now;

            _dbContext.TbProduto.Update(existingProduto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarProduto(Produto produto)
        {
            // Obter os registros relacionados na tabela TBPreco
            var precos = _dbContext.TbPreco.Where(p => p.IdProduto == produto.Id).ToList();

            // Remover os registros relacionados
            _dbContext.TbPreco.RemoveRange(precos);

            // Remover o produto
            _dbContext.TbProduto.Remove(produto);

            // Salvar as mudanças
            await _dbContext.SaveChangesAsync();
        }
    }
}
