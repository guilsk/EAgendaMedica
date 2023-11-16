using EAgendaMedica.Dominio.ModuloAtividade;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EAgendaMedica.TestesUnitarios.Domínio.ModuloAtividade {
    [TestClass]
    public class AtividadeTests {
        private Atividade a1;
        private ValidadorAtividade validadorAtividade;

        [TestInitialize] 
        public void Init() { 
            a1 = new Atividade(); 
            validadorAtividade = new ValidadorAtividade();
        }

        [TestMethod]
        public void Não_deve_aceitar_valor_nulo() {
            ValidationResult resultado = validadorAtividade.Validate(a1);
            resultado.IsValid.Should().BeFalse();
        }
    }
}
