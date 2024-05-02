using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class CategoriaService
    {
        private readonly ConnectionDB _dbContext;

        public CategoriaService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _dbContext.TbCategoria.ToListAsync();
        }

        public async Task<Categoria?> GetCategoriaById(int id)
        {
            return await _dbContext.TbCategoria.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Categoria> CriarCategoria(Categoria categoria)
        {
            _dbContext.TbCategoria.Add(categoria);
            await _dbContext.SaveChangesAsync();

            return categoria;
        }

        public async Task AtualizarCategoria(Categoria existingCategoria, Categoria categoria)
        {
            existingCategoria.Nome = categoria.Nome;
            existingCategoria.Data_modificacao = DateTime.Now;

            _dbContext.TbCategoria.Update(existingCategoria);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarCategoria(Categoria categoria)
        {
            _dbContext.TbCategoria.Remove(categoria);
            await _dbContext.SaveChangesAsync();
        }
    }
}
