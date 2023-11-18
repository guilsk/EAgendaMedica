using EAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EAgendaMedica.TestesUnitarios.Domínio.ModuloMedico {
    [TestClass]
    public class MedicoTests {
        [TestMethod]
        public void Medico_Deve_Ser_Valido_Com_Dados_Corretos() {
            // Arrange
            var medico = new Medico {
                Nome = "Dr. Teste III",
                Crm = "12345-AZ"
            };

            // Act
            var resultadoValidacao = new ValidadorMedico().Validate(medico);

            // Assert
            resultadoValidacao.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Medico_Deve_Ser_Invalido_Com_Nome_Nulo() {
            // Arrange
            var medico = new Medico {
                Crm = "12345-AZ"
            };

            // Act
            var resultadoValidacao = new ValidadorMedico().Validate(medico);

            // Assert
            resultadoValidacao.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Medico_Deve_Ser_Invalido_Com_Crm_Nulo() {
            // Arrange
            var medico = new Medico {
                Nome = "Dr. Teste III",
            };

            // Act
            var resultadoValidacao = new ValidadorMedico().Validate(medico);

            // Assert
            resultadoValidacao.IsValid.Should().BeFalse();
        }
    }
}
