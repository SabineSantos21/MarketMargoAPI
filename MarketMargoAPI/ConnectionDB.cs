using MarketMargoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMargoAPI
{
    public class ConnectionDB : DbContext
    {
        public DbSet<Usuario> TbUsuario { get; set; }

        public ConnectionDB(DbContextOptions<ConnectionDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("tbusuario").HasKey(u => u.Id);
        }
    }
}
