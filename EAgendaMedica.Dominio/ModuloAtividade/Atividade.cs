using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloMedico;

namespace EAgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : Entidade{
        public string Descricao { get; set; }
        public string NomePaciente { get; set; }
        public string CpfPaciente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }
        public Medico MedicoConsulta { get; set; }
        public List<Medico> MedicosCirurgia { get; set; }

    }
}
