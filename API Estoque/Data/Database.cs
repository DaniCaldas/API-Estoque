using API_Estoque.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace API_Estoque.Data
{
    public class Database: DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }
        public Database() : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseNpgsql(configuration.GetConnectionString("NeonConnection"));
            }
        }

        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Movimentacoes> Movimentacoes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
