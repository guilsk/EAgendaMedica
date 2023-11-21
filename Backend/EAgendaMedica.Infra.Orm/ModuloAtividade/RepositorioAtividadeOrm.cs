using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioAtividadeOrm : RepositorioBase<Atividade>, IRepositorioAtividade {
        public RepositorioAtividadeOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia) { }

        public override Atividade SelecionarPorId(Guid id) => registros.SingleOrDefault(x => x.Id == id);

        public override async Task<Atividade> SelecionarPorIdAsync(Guid id) => await registros.Include(x => x.Medicos).SingleOrDefaultAsync(x => x.Id == id);
    }
}
