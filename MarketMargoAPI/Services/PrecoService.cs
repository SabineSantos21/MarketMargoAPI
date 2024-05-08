using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class PrecoService
    {
        private readonly ConnectionDB _dbContext;

        public PrecoService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Preco>> GetPrecos()
        {
            return await _dbContext.TbPreco.ToListAsync();
        }

        public async Task<Preco?> GetPrecoById(int id)
        {
            return await _dbContext.TbPreco.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Preco? GetPrecoByProdutoId(int produto_id)
        {
            return _dbContext.TbPreco.Where(p => p.IdProduto == produto_id).OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public async Task<Preco> CriarPreco(Preco preco)
        {
            _dbContext.TbPreco.Add(preco);
            await _dbContext.SaveChangesAsync();

            return preco;
        }

        public async Task AtualizarPreco(Preco existingPreco, Preco preco)
        {
            existingPreco.IdProduto = preco.IdProduto;
            existingPreco.PorcentagemAumento = preco.PorcentagemAumento;
            existingPreco.Valor = preco.Valor;
            existingPreco.Ativo = preco.Ativo;
            existingPreco.Data_modificacao = DateTime.Now;

            _dbContext.TbPreco.Update(existingPreco);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarPreco(Preco preco)
        {
            _dbContext.TbPreco.Remove(preco);
            await _dbContext.SaveChangesAsync();
        }
    }
}
