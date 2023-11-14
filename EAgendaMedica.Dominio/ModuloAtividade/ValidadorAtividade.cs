using FluentValidation;

namespace EAgendaMedica.Dominio.ModuloAtividade
{
    public class ValidadorAtividade : AbstractValidator<Atividade> {
        public ValidadorAtividade() { 
            
            RuleFor(x => x.Descricao).NotNull().NotEmpty();
            RuleFor(x => x.Data).NotNull().NotEmpty();
            RuleFor(x => x.HoraInicio).NotNull().NotEmpty();
            RuleFor(x => x.HoraFim).NotNull().NotEmpty();
            RuleFor(x => x.TipoAtividade).IsInEnum();

        }
    }
}