using API_Estoque.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Estoque.Data
{
    public class Database: DbContext
    {
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Movimentacoes> Movimentacoes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        private string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=API;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection).UseLazyLoadingProxies(); 
        }
    }
}
