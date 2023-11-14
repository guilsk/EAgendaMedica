using EAgendaMedica.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgendaMedica.Infra.Orm.Compartilhado {
    public class RepositorioBase<TEntidade> where TEntidade : Entidade{
        protected EAgendaDbContext dbContext;
        protected DbSet<TEntidade> registros;

        public RepositorioBase(IContextoPersistencia ctx) {
            this.dbContext = (EAgendaDbContext)ctx;
            this.registros = dbContext.Set<TEntidade>();
        }

        public async Task<bool> InserirAsync(TEntidade registro) {
            await registros.AddAsync(registro);

            return true;
        }

        public void Editar(TEntidade registro) {
            registros.Update(registro);
        }

        public void Excluir(TEntidade registro) {
            registros.Remove(registro);
        }

        public virtual TEntidade SelecionarPorId(Guid id) {
            return registros.SingleOrDefault(x => x.Id == id);
        }

        public async virtual Task<TEntidade> SelecionarPorIdAsync(Guid id) {
            return await registros.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntidade>> SelecionarTodosAsync() {
            return await registros.ToListAsync();
        }
    }
}
