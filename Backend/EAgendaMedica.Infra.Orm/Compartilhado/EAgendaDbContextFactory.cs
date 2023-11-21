using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EAgendaMedica.Infra.Orm.Compartilhado {
    public class EAgendaDbContextFactory : IDesignTimeDbContextFactory<EAgendaDbContext> {
        public EAgendaDbContext CreateDbContext(string[] args) {
            var builder = new DbContextOptionsBuilder<EAgendaDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=EAgendaMedica;Integrated Security=True");

            return new EAgendaDbContext(builder.Options);
        }
    }
}
