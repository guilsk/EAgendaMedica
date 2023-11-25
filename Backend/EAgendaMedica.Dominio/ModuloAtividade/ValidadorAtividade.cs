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
            RuleFor(a => a.Medicos.Count).Equal(1).When(a => a.TipoAtividade == TipoAtividadeEnum.Consulta).WithMessage("A consulta só deve ter um médico selecionado.");
            RuleFor(a => a.Medicos).NotEmpty().WithMessage("Pelo Menos um médico deve ser selecionado.");

        }

        public bool ConfirmarSeAtividadeTemConflitoDeHorario(Atividade atividade) {

            foreach (Medico medico in atividade.Medicos ?? Enumerable.Empty<Medico>()) {
                foreach (Atividade atividadeDoMedico in medico.Atividades ?? Enumerable.Empty<Atividade>()) {

                    if (atividade.Id != atividadeDoMedico.Id) {
                        // Verifica se os médicos da atividade possuem outra atividade no mesmo dia ou no dia anterior
                        if ((atividade.Data == atividadeDoMedico.Data) ||
                            (atividadeDoMedico.HoraInicio >= atividadeDoMedico.HoraFim && atividadeDoMedico.Data.AddDays(1) == atividade.Data)) {

                            if (atividade.HoraInicio >= atividadeDoMedico.HoraInicio && atividade.HoraInicio <= atividadeDoMedico.HoraFim ||
                                atividade.HoraFim >= atividadeDoMedico.HoraInicio && atividade.HoraFim <= atividadeDoMedico.HoraFim) {
                                return true;
                            }

                            TimeSpan diferenca = atividadeDoMedico.HoraFim - atividade.HoraInicio;

                            // Restante da lógica de verificação de conflito
                            if ((atividade.TipoAtividade == TipoAtividadeEnum.Consulta && diferenca.TotalMinutes < 20) ||
                                (atividade.TipoAtividade == TipoAtividadeEnum.Cirurgia && diferenca.TotalMinutes < 240)) {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

    }
}