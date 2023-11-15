using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloMedico;

namespace EAgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : Entidade{
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; } //Limite de 24h de Consulta ou Cirurgia
        public TipoAtividadeEnum TipoAtividade { get; set; }
        public List<Medico> Medicos { get; set; }

    }
}
