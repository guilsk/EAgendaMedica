using EAgendaMedica.Dominio.ModuloMedico;
using FluentValidation;

namespace EAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorAtividade : AbstractValidator<Atividade> {
        public ValidadorAtividade() { 
            
            RuleFor(x => x.Descricao).NotNull().NotEmpty().WithMessage("Descrição não pode ser vazia.");
            RuleFor(x => x.Data).NotNull().NotEmpty();
            RuleFor(x => x.HoraInicio).NotNull().NotEmpty();
            RuleFor(x => x.HoraFim).NotNull().NotEmpty();
            RuleFor(x => x.TipoAtividade).IsInEnum();
            When(x => x.TipoAtividade == TipoAtividadeEnum.Consulta, () => {
                RuleFor(x => x.Medicos.Count).Equals(1);
            });
            When(x => x.TipoAtividade == TipoAtividadeEnum.Cirurgia, () => {
                RuleFor(x => x.Medicos.Count).NotNull().NotEmpty();
            });
            
        }

        public bool ConfirmarSeAtividadeTemConflitoDeHorario(Atividade atividade) {
            if(atividade.Medicos != null)
            foreach (Medico medico in atividade.Medicos) {
                if (medico.Atividades.Any()) {
                    foreach (Atividade atividadeDoMedico in medico.Atividades) {
                        if (atividade.Data == atividadeDoMedico.Data) {
                            if (atividade.TipoAtividade == TipoAtividadeEnum.Consulta) {
                                if (atividade.HoraInicio.Minutes.CompareTo(atividadeDoMedico.HoraFim.Minutes) < 20) return true;
                            } else {
                                if (atividade.HoraInicio.Minutes.CompareTo(atividadeDoMedico.HoraFim.Minutes) < 240) return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

    }
}