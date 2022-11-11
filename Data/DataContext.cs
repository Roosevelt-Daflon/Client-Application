using Client_Application.Model;
using Microsoft.EntityFrameworkCore;

namespace Client_Application.Data
{
	public class DataContext : DbContext
	{
		protected readonly IConfiguration _Configuration;

		public DbSet<Cliente>? Clientes { get; set; }
		public DbSet<Endereco>? Enderecos { get; set; }

		public DataContext(IConfiguration configuration)
		{
			_Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(_Configuration.GetConnectionString("WebApiDatabase"));
		}
	}
}
