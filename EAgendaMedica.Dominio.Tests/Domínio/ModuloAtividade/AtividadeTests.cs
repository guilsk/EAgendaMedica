using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
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

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_Descricao() {
            // Arrange
            var validador = new ValidadorAtividade();

            // Act
            ValidationResult resultado = validador.Validate(new Atividade { Descricao = "" });

            // Assert
            resultado.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_Data() {
            // Arrange
            var validador = new ValidadorAtividade();

            // Act
            ValidationResult resultado = validador.Validate(new Atividade { Data = DateTime.MinValue });

            // Assert
            resultado.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_HoraInicio() {
            // Arrange
            var validador = new ValidadorAtividade();

            // Act
            ValidationResult resultado = validador.Validate(new Atividade { HoraInicio = TimeSpan.MinValue });

            // Assert
            resultado.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_HoraFim() {
            // Arrange
            var validador = new ValidadorAtividade();

            // Act
            ValidationResult resultado = validador.Validate(new Atividade { HoraFim = TimeSpan.MinValue });

            // Assert
            resultado.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_TipoAtividade() {
            // Arrange
            var validador = new ValidadorAtividade();

            // Act
            ValidationResult resultado = validador.Validate(new Atividade { TipoAtividade = (TipoAtividadeEnum)3 });

            // Assert
            resultado.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_Consulta_Com_Medicos_Contagem_Diferente_De_1() {
            // Arrange
            var validador = new ValidadorAtividade();
            var atividade = new Atividade {
                TipoAtividade = TipoAtividadeEnum.Consulta,
                Medicos = new List<Medico> { new Medico() }
            };

            // Act
            ValidationResult resultado = validador.Validate(atividade);

            // Assert
            resultado.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void ValidadorAtividade_Deve_Ter_Rule_For_Cirurgia_Com_Medicos_Null_Ou_Vazio() {
            // Arrange
            var validador = new ValidadorAtividade();
            var atividade = new Atividade {
                TipoAtividade = TipoAtividadeEnum.Cirurgia,
                Medicos = new List<Medico>()
            };

            // Act
            ValidationResult resultado = validador.Validate(atividade);

            // Assert
            resultado.IsValid.Should().BeFalse();

        }



    }
}
