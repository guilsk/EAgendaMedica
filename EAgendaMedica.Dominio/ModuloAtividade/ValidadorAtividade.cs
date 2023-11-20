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

            RuleFor(atividade => atividade)
            .Custom((atividade, context) => {
                if (atividade.TipoAtividade == TipoAtividadeEnum.Consulta && atividade.Medicos.Count != 1)
                    context.AddFailure("Consulta deve ter exatamente um médico.");

                if (atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia && !atividade.Medicos.Any())
                    context.AddFailure("Cirurgia deve ter pelo menos um médico.");
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