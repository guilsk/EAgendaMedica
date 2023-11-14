using EAgendaMedica.Dominio.Compartilhado;
using FluentValidation;

namespace EAgendaMedica.Dominio.ModuloMedico {

    public class ValidadorMedico : AbstractValidator<Medico>{
        public ValidadorMedico() {

            RuleFor(x => x.Nome).NotNull().NotEmpty();
            RuleFor(x => x.Crm).Crm().NotNull().NotEmpty();

        }
    }
}
