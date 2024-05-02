using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class GondolaService
    {
        private readonly ConnectionDB _dbContext;

        public GondolaService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Gondola>> GetGondolas()
        {
            return await _dbContext.TbGondola.ToListAsync();
        }

        public async Task<Gondola?> GetGondolaById(int id)
        {
            return await _dbContext.TbGondola.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Gondola> CriarGondola(Gondola gondola)
        {
            _dbContext.TbGondola.Add(gondola);
            await _dbContext.SaveChangesAsync();

            return gondola;
        }

        public async Task AtualizarGondola(Gondola existingGondola, Gondola gondola)
        {
            existingGondola.Nome = gondola.Nome;
            existingGondola.Capacidade = gondola.Capacidade;
            existingGondola.Id_Categoria = gondola.Id_Categoria;
            existingGondola.Ativo = gondola.Ativo;
            existingGondola.Data_modificacao = DateTime.Now;

            _dbContext.TbGondola.Update(existingGondola);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarGondola(Gondola gondola)
        {
            _dbContext.TbGondola.Remove(gondola);
            await _dbContext.SaveChangesAsync();
        }
    }
}
