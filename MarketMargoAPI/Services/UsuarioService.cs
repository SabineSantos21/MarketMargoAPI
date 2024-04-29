using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI.Services
{
    public class UsuarioService
    {
        private readonly ConnectionDB _dbContext;

        public UsuarioService(ConnectionDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _dbContext.TbUsuario.ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioById(int id)
        {
            return await _dbContext.TbUsuario.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            _dbContext.TbUsuario.Add(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task AtualizarUsuario(Usuario existingUsuario, Usuario usuario)
        {
            existingUsuario.Nome = usuario.Nome;
            existingUsuario.Ativo = usuario.Ativo;
            existingUsuario.Data_modificacao = DateTime.Now;

            _dbContext.TbUsuario.Update(existingUsuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarUsuario(Usuario usuario)
        {
            _dbContext.TbUsuario.Remove(usuario);
            await _dbContext.SaveChangesAsync();
        }
    }
}
