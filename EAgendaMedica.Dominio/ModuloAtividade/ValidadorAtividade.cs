using EAgendaMedica.Dominio.ModuloMedico;
using FluentValidation;

namespace EAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorAtividade : AbstractValidator<Atividade> {
        public ValidadorAtividade() { 
            
            RuleFor(x => x.Descricao).NotNull().NotEmpty();
            RuleFor(x => x.Data).NotNull().NotEmpty();
            RuleFor(x => x.HoraInicio).NotNull();
            RuleFor(x => x.HoraFim).NotNull();
            RuleFor(x => x.TipoAtividade).IsInEnum().Must(tipo => tipo == TipoAtividadeEnum.Cirurgia || tipo == TipoAtividadeEnum.Consulta);
            When(x => x.TipoAtividade == TipoAtividadeEnum.Consulta, () => {
                RuleFor(x => x.Medicos.Count).Equals(1);
            });
            When(x => x.TipoAtividade == TipoAtividadeEnum.Cirurgia, () => {
                RuleFor(x => x.Medicos).NotNull().NotEmpty();
            });
            
        }

        public bool ConfirmarSeAtividadeTemConflitoDeHorario(Atividade atividade) {

            foreach (Medico medico in atividade.Medicos ?? Enumerable.Empty<Medico>()) {
                foreach (Atividade atividadeDoMedico in medico.Atividades ?? Enumerable.Empty<Atividade>()) {

                    // Verifica se os médicos da atividade possuem outra atividade no mesmo dia ou no dia anterior
                    if ((atividade.Data == atividadeDoMedico.Data) || 
                        (atividadeDoMedico.HoraInicio >= atividadeDoMedico.HoraFim && atividadeDoMedico.Data.AddDays(1) == atividade.Data)) {
                        TimeSpan diferenca = atividade.HoraFim - atividadeDoMedico.HoraInicio;

                        // Restante da lógica de verificação de conflito
                        if ((atividade.TipoAtividade == TipoAtividadeEnum.Consulta && diferenca.TotalMinutes < 20) ||
                            (atividade.TipoAtividade != TipoAtividadeEnum.Consulta && diferenca.TotalMinutes < 240)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}