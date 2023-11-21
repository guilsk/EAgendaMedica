using FluentValidation;

namespace EAgendaMedica.Dominio.Compartilhado {
    public static class RuleBuilderExtensions {

        public static IRuleBuilder<T, string> Crm<T>(this IRuleBuilder<T, string> ruleBuilder) {
            var options = ruleBuilder.Matches(@"^\d{5}-[A-Z]{2}$");
            return options;
        }

    }
}
