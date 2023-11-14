using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgendaMedica.Infra.Orm.ModuloMedico {
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia) {}

        public override Medico SelecionarPorId(Guid id) => registros.SingleOrDefault(x => x.Id == id);

        public override async Task<Medico> SelecionarPorIdAsync(Guid id) => await registros.Include(x => x.Atividades).SingleOrDefaultAsync(x => x.Id == id);
    }
}
