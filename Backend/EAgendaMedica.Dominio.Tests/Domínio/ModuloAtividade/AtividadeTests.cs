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

        [TestMethod]
        public void Teste_conflito_de_horario() {
            Guid medicoId = Guid.NewGuid();
            var atividade1 = new Atividade {
                Descricao = "Atividade 1",
                Data = DateTime.Today,
                HoraInicio = TimeSpan.Parse("12:00:00"),
                HoraFim = TimeSpan.Parse("13:00:00"),
                TipoAtividade = TipoAtividadeEnum.Consulta,
                Medicos = { new Medico { Id = medicoId, Nome = "Dr. Teste", Crm = "12345-SC" } }

            };
            var atividade2 = new Atividade {
                Descricao = "Atividade 2",
                Data = DateTime.Today,
                HoraInicio = TimeSpan.Parse("12:30:00"),
                HoraFim = TimeSpan.Parse("13:00:00"),
                TipoAtividade = TipoAtividadeEnum.Consulta,
                Medicos = { new Medico { Id = medicoId, Nome = "Dr. Teste", Crm = "12345-SC", Atividades = {atividade1} } }
            };

            bool temConflito = ConfirmarSeAtividadeTemConflitoDeHorario(atividade2);

            temConflito.Should().BeTrue();
        }

        private bool ConfirmarSeAtividadeTemConflitoDeHorario(Atividade atividade) {

            foreach (Medico medico in atividade.Medicos ?? Enumerable.Empty<Medico>()) {
                foreach (Atividade atividadeDoMedico in medico.Atividades ?? Enumerable.Empty<Atividade>()) {

                    if (atividade.Id != atividadeDoMedico.Id) {
                        // Verifica se os médicos da atividade possuem outra atividade no mesmo dia ou no dia anterior
                        if ((atividade.Data == atividadeDoMedico.Data) ||
                            (atividadeDoMedico.HoraInicio >= atividadeDoMedico.HoraFim && atividadeDoMedico.Data.AddDays(1) == atividade.Data)) {

                            if(atividade.HoraInicio >= atividadeDoMedico.HoraInicio && atividade.HoraInicio <= atividadeDoMedico.HoraFim) {
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
