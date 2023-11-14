using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloAtividade;

namespace EAgendaMedica.Dominio.ModuloMedico {
    public class Medico : Entidade {
        public string Nome { get; set; }
        public string Crm { get; set; }
        public List<Atividade> Atividades { get; set; }
    }
}
