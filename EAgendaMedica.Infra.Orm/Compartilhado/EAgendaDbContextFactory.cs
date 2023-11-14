using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EAgendaMedica.Infra.Orm.Compartilhado {
    public class EAgendaDbContextFactory : IDesignTimeDbContextFactory<EAgendaDbContext> {
        public EAgendaDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<EAgendaDbContext>();

            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(connectionString);

            var dbContext = new EAgendaDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
