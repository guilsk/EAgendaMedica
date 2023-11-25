using EAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EAgendaMedica.ConsoleApp {
    internal class Program {
        static void Main(string[] args) {

            var optionsBuilder = new DbContextOptionsBuilder<EAgendaDbContext>();

            IConfiguration configuracao = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=EAgendaMedica;Integrated Security=True");

            var dbContext = new EAgendaDbContext(optionsBuilder.Options);

            dbContext.SaveChanges();
        }
    }
}