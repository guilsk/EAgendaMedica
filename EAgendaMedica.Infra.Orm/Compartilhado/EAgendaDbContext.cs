using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace EAgendaMedica.Infra.Orm.Compartilhado {
    public class EAgendaDbContext : DbContext, IContextoPersistencia {
        public EAgendaDbContext(DbContextOptions options) : base(options) { }
        public async Task<bool> GravarAsync() {
            await SaveChangesAsync();
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new MapeadorMedicoOrm());

            //modelBuilder.ApplyConfiguration(new MapeadorAtividadeOrm());

            base.OnModelCreating(modelBuilder);
        }
    }
}
