using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI
{
    public class ConnectionDB : DbContext
    {
        public DbSet<Usuario> TbUsuario { get; set; }

        public DbSet<Produto> TbProduto { get; set; }

        public DbSet<Preco> TbPreco { get; set; }

        public DbSet<Categoria> TbCategoria { get; set; }

        public DbSet<Gondola> TbGondola { get; set; }

        public DbSet<Caixa> TbCaixa { get; set; }

        public ConnectionDB(DbContextOptions<ConnectionDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("tbusuario").HasKey(u => u.Id);
            
            modelBuilder.Entity<Produto>().ToTable("tbproduto").HasKey(u => u.Id);
            
            modelBuilder.Entity<Preco>().ToTable("tbpreco").HasKey(u => u.Id);
            
            modelBuilder.Entity<Categoria>().ToTable("tbcategoria").HasKey(u => u.Id);
            
            modelBuilder.Entity<Gondola>().ToTable("tbgondola").HasKey(u => u.Id);

            modelBuilder.Entity<Caixa>().ToTable("tbcaixa").HasKey(u => u.Id);
        }
    }
}
