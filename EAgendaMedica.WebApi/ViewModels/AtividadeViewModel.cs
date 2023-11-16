using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;

namespace EAgendaMedica.WebApi.ViewModels {

    public class FormsAtividadeViewModel {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }
        public List<Guid> MedicosId { get; set; }
    }

    public class InserirAtividadeViewModel : FormsAtividadeViewModel {}

    public class EditarAtividadeViewModel : FormsAtividadeViewModel {
    }

    public class ListarAtividadeViewModel {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }
        public List<Medico> Medicos { get; set; }
    }

    public class VisualizarAtividadeViewModel {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }
        public List<Medico> Medicos { get; set; }
    }
}
