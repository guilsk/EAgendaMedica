using FluentAssertions;
using FluentResults;
using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloMedico;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EAgendaMedica.Aplicacao.ModuloMedico;
using NSubstitute;

namespace EAgendaMedica.TestesUnitarios.Aplicação.ModuloMedico {
    [TestClass]
    public class ServicoMedicoTests {
        [TestMethod]
        public async Task InserirAsync_Deve_Inserir_Medico_Corretamente() {
            // Arrange
            var medico = new Medico();
            medico.Nome = "Dr. Teste III";
            medico.Crm = "12345-AZ";
            var mockRepositorio = new Mock<IRepositorioMedico>();
            var mockContextoPersistencia = new Mock<IContextoPersistencia>();
            var servico = new ServicoMedico(mockRepositorio.Object, mockContextoPersistencia.Object);

            // Act
            Result<Medico> resultado = await servico.InserirAsync(medico);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            mockRepositorio.Verify(r => r.InserirAsync(medico), Times.Once);
            mockContextoPersistencia.Verify(c => c.GravarAsync(), Times.Once);
        }

        [TestMethod]
        public async Task InserirAsync_Deve_Retornar_Erro_Quando_Medico_Invalido() {
            // Arrange
            var medico = new Medico();
            var mockRepositorio = new Mock<IRepositorioMedico>();
            var mockContextoPersistencia = new Mock<IContextoPersistencia>();
            var servico = new ServicoMedico(mockRepositorio.Object, mockContextoPersistencia.Object);

            // Act
            Result<Medico> resultado = await servico.InserirAsync(medico);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().NotBeEmpty();
            mockRepositorio.Verify(r => r.InserirAsync(medico), Times.Never);
            mockContextoPersistencia.Verify(c => c.GravarAsync(), Times.Never);
        }

        [TestMethod]
        public async Task EditarAsync_Deve_Editar_Medico_Corretamente() {
            // Arrange
            var repositorioMedico = Substitute.For<IRepositorioMedico>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico, contextoPersistencia);

            var medico = new Medico {
                Id = Guid.NewGuid(), // Id válido
                Nome = "Nome do Médico",
                Crm = "12345-AZ"
            };

            // Act
            var resultado = await servicoMedico.EditarAsync(medico);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            repositorioMedico.Received().Editar(medico);
            await contextoPersistencia.Received().GravarAsync();
        }

        [TestMethod]
        public async Task EditarAsync_Deve_Falhar_Com_Medico_Invalido() {
            // Arrange
            var repositorioMedico = Substitute.For<IRepositorioMedico>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico, contextoPersistencia);

            var medico = new Medico(); // Medico inválido

            // Act
            var resultado = await servicoMedico.EditarAsync(medico);

            // Assert
            resultado.IsFailed.Should().BeTrue();
            repositorioMedico.DidNotReceive().Editar(Arg.Any<Medico>());
            await contextoPersistencia.DidNotReceive().GravarAsync();
        }

        [TestMethod]
        public async Task ExcluirAsync_Deve_Excluir_Medico_Corretamente() {
            // Arrange
            var repositorioMedico = Substitute.For<IRepositorioMedico>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico, contextoPersistencia);

            var medicoId = Guid.NewGuid(); // Id válido
            var medico = new Medico { Id = medicoId };

            // Garantir que o médico está presente no repositório
            repositorioMedico.SelecionarPorIdAsync(medicoId).Returns(medico);

            // Act
            var resultado = await servicoMedico.ExcluirAsync(medicoId);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            repositorioMedico.Received().Excluir(Arg.Any<Medico>());
            await contextoPersistencia.Received().GravarAsync();
        }

        [TestMethod]
        public async Task ExcluirAsync_Deve_Falhar_Com_Medico_Nao_Encontrado() {
            // Arrange
            var repositorioMedico = Substitute.For<IRepositorioMedico>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico, contextoPersistencia);

            var medicoId = Guid.NewGuid(); // Id válido, mas sem correspondência no repositório

            // Act
            var resultado = await servicoMedico.ExcluirAsync(medicoId);

            // Assert
            resultado.IsFailed.Should().BeTrue();
            repositorioMedico.DidNotReceive().Excluir(Arg.Any<Medico>());
            await contextoPersistencia.DidNotReceive().GravarAsync();
        }

        [TestMethod]
        public async Task SelecionarTodosAsync_Deve_Retornar_Lista_De_Medicos() {
            // Arrange
            var repositorioMedico = Substitute.For<IRepositorioMedico>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico, contextoPersistencia);

            var listaMedicos = new List<Medico> { new Medico(), new Medico() };

            repositorioMedico.SelecionarTodosAsync().Returns(listaMedicos);

            // Act
            var resultado = await servicoMedico.SelecionarTodosAsync();

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Should().BeEquivalentTo(listaMedicos);
        }

        [TestMethod]
        public async Task SelecionarPorIdAsync_Deve_Retornar_Medico_Por_Id() {
            // Arrange
            var repositorioMedico = Substitute.For<IRepositorioMedico>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico, contextoPersistencia);

            var medicoId = Guid.NewGuid(); // Id válido
            var medicoEsperado = new Medico { Id = medicoId, Nome = "Nome do Médico", Crm = "00000-AA" };

            // Garantir que o médico é retornado ao chamar o método SelecionarPorIdAsync
            repositorioMedico.SelecionarPorIdAsync(medicoId).Returns(medicoEsperado);

            // Act
            var resultado = await servicoMedico.SelecionarPorIdAsync(medicoId);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            resultado.Value.Should().BeEquivalentTo(medicoEsperado);
        }

        [TestMethod]
        public void ValidarMedico_Deve_Retornar_ResultOk_Com_Medico_Valido() {
            // Arrange
            var repositorioMedico = new Mock<IRepositorioMedico>();
            var contextoPersistencia = new Mock<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico.Object, contextoPersistencia.Object);

            var medico = new Medico {
                Nome = "Nome do Médico",
                Crm = "12345-AZ"
            };

            // Act
            var resultado = servicoMedico.ValidarMedico(medico);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void ValidarMedico_Deve_Retornar_ResultFail_Com_Medico_Invalido() {
            // Arrange
            var repositorioMedico = new Mock<IRepositorioMedico>();
            var contextoPersistencia = new Mock<IContextoPersistencia>();
            var servicoMedico = new ServicoMedico(repositorioMedico.Object, contextoPersistencia.Object);

            var medico = new Medico(); // Medico inválido

            // Act
            var resultado = servicoMedico.ValidarMedico(medico);

            // Assert
            resultado.IsFailed.Should().BeTrue();
        }
    
    }
}