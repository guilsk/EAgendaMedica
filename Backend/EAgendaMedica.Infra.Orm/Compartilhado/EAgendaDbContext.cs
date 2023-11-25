using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.Infra.Orm.ModuloAtividade;
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

            modelBuilder.ApplyConfiguration(new MapeadorAtividadeOrm());

            var novoMedico = new Medico { Nome = "Dr. Teste I", Crm = "00000-AA" };
            var novoMedico2 = new Medico { Nome = "Dr. Teste II", Crm = "09876-ZZ" };
            var novoMedico3 = new Medico { Nome = "Dr. Teste III", Crm = "12345-AZ" };

            modelBuilder.Entity<Medico>().HasData(novoMedico);
            modelBuilder.Entity<Medico>().HasData(novoMedico2);
            modelBuilder.Entity<Medico>().HasData(novoMedico3);

            base.OnModelCreating(modelBuilder);
        }
        public async Task InicializarDadosDeTesteAsync() {
            if (!Set<Medico>().Any()) {
                Set<Medico>().Add(new Medico { Nome = "Dr. Teste I", Crm = "00000-AA" });
                Set<Medico>().Add(new Medico { Nome = "Dr. Teste II", Crm = "09876-ZZ" });
                Set<Medico>().Add(new Medico { Nome = "Dr. Teste III", Crm = "12345-AZ" });

                await SaveChangesAsync();
            }
        }
    }
}
