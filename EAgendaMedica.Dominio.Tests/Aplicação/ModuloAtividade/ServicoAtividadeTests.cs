using EAgendaMedica.Aplicacao.ModuloAtividade;
using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using FluentResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;

namespace EAgendaMedica.TestesUnitarios.Aplicação.ModuloAtividade {
    [TestClass]
    public class ServicoAtividadeTests {
        [TestMethod]
        public async Task InserirAsync_Deve_Inserir_Atividade_Corretamente() {
            // Arrange
            var repositorioAtividade = Substitute.For<IRepositorioAtividade>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoAtividade = new ServicoAtividade(repositorioAtividade, contextoPersistencia);

            var atividade = new Atividade {
                Descricao = "Consulta",
                Data = DateTime.Now,
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFim = new TimeSpan(11, 0, 0),
                TipoAtividade = TipoAtividadeEnum.Consulta,
                Medicos = new List<Medico> { new Medico { Nome = "Dr. Smith", Crm = "12345-AA" } }
            };

            // Act
            var resultado = await servicoAtividade.InserirAsync(atividade);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            await repositorioAtividade.Received().InserirAsync(atividade);
            await contextoPersistencia.Received().GravarAsync();
        }

        [TestMethod]
        public async Task InserirAsync_Deve_Retornar_Erro_Quando_Atividade_Invalida() {
            // Arrange
            var atividade = new Atividade();
            var mockRepositorio = new Mock<IRepositorioAtividade>();
            var mockContextoPersistencia = new Mock<IContextoPersistencia>();
            var servico = new ServicoAtividade(mockRepositorio.Object, mockContextoPersistencia.Object);

            // Act
            Result<Atividade> resultado = await servico.InserirAsync(atividade);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            resultado.Errors.Should().NotBeEmpty();
            mockRepositorio.Verify(r => r.InserirAsync(atividade), Times.Never);
            mockContextoPersistencia.Verify(c => c.GravarAsync(), Times.Never);
        }

        [TestMethod]
        public async Task InserirAsync_Deve_Retornar_Erro_Quando_Houver_Conflito_De_Horario() {
            // Arrange
            var atividade1 = new Atividade {
                Descricao = "Consulta com Dr. Teste",
                Data = DateTime.Now.Date,
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFim = new TimeSpan(11, 0, 0),
                TipoAtividade = TipoAtividadeEnum.Consulta
            };

            var atividade2 = new Atividade {
                Descricao = "Consulta com Dr. Teste",
                Data = DateTime.Now.Date,
                HoraInicio = new TimeSpan(10, 30, 0),  // Conflito com a atividade1
                HoraFim = new TimeSpan(11, 30, 0),
                TipoAtividade = TipoAtividadeEnum.Consulta
            };

            var mockRepositorio = Substitute.For<IRepositorioAtividade>();
            var mockContextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servico = new ServicoAtividade(mockRepositorio, mockContextoPersistencia);

            // Act
            Result<Atividade> resultado1 = await servico.InserirAsync(atividade1);
            Result<Atividade> resultado2 = await servico.InserirAsync(atividade2);

            // Assert
            resultado1.IsSuccess.Should().BeTrue();
            resultado2.IsSuccess.Should().BeFalse();
            resultado2.Errors.Should().ContainSingle(e => e.Message == "Esta atividade tem conflito de horário com outra atividade desse mesmo médico.");
            mockRepositorio.Received(1).InserirAsync(Arg.Is<Atividade>(a => a == atividade1));
            mockRepositorio.DidNotReceive().InserirAsync(Arg.Is<Atividade>(a => a == atividade2));
            mockContextoPersistencia.Received(1).GravarAsync();
        }

        [TestMethod]
        public async Task EditarAsync_Deve_Editar_Atividade_Corretamente() {
            // Arrange
            var repositorioAtividade = Substitute.For<IRepositorioAtividade>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoAtividade = new ServicoAtividade(repositorioAtividade, contextoPersistencia);

            var atividade = new Atividade { Id = Guid.NewGuid(), Descricao = "Consulta", Data = DateTime.Now, HoraInicio = TimeSpan.FromHours(10), HoraFim = TimeSpan.FromHours(11) };

            // Act
            var resultado = await servicoAtividade.EditarAsync(atividade);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            repositorioAtividade.Received().Editar(Arg.Any<Atividade>());
            await contextoPersistencia.Received().GravarAsync();
        }

        [TestMethod]
        public async Task EditarAsync_Deve_Retornar_Falha_Quando_Atividade_Nao_Encontrada() {
            // Arrange
            var repositorioAtividade = Substitute.For<IRepositorioAtividade>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoAtividade = new ServicoAtividade(repositorioAtividade, contextoPersistencia);

            var atividadeId = Guid.NewGuid(); // Id válido

            // Configurando o repositório para retornar null, simulando que a atividade não foi encontrada
            repositorioAtividade.SelecionarPorIdAsync(atividadeId).Returns(Task.FromResult<Atividade>(null));

            // Act
            var resultado = await servicoAtividade.EditarAsync(new Atividade { Id = atividadeId, /* outros atributos */ });

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            repositorioAtividade.DidNotReceive().Editar(Arg.Any<Atividade>());
            await contextoPersistencia.DidNotReceive().GravarAsync();
        }

        [TestMethod]
        public async Task ExcluirAsync_Deve_Excluir_Atividade_Corretamente() {
            // Arrange
            var repositorioAtividade = Substitute.For<IRepositorioAtividade>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoAtividade = new ServicoAtividade(repositorioAtividade, contextoPersistencia);

            var atividadeId = Guid.NewGuid(); // Id válido
            var atividade = new Atividade { Id = atividadeId, Descricao = "Consulta"}; // Preencha os detalhes da atividade

            repositorioAtividade.SelecionarPorIdAsync(atividadeId).Returns(Task.FromResult(atividade));

            // Act
            var resultado = await servicoAtividade.ExcluirAsync(atividadeId);

            // Assert
            resultado.IsSuccess.Should().BeTrue();
            repositorioAtividade.Received().Excluir(atividade);
            await contextoPersistencia.Received().GravarAsync();
        }

        [TestMethod]
        public async Task ExcluirAsync_Deve_Retornar_Falha_Quando_Atividade_Nao_Encontrada() {
            // Arrange
            var repositorioAtividade = Substitute.For<IRepositorioAtividade>();
            var contextoPersistencia = Substitute.For<IContextoPersistencia>();
            var servicoAtividade = new ServicoAtividade(repositorioAtividade, contextoPersistencia);

            var atividadeId = Guid.NewGuid(); // Id válido

            // Configurando o repositório para retornar null, simulando que a atividade não foi encontrada
            repositorioAtividade.SelecionarPorIdAsync(atividadeId).Returns(Task.FromResult<Atividade>(null));

            // Act
            var resultado = await servicoAtividade.ExcluirAsync(atividadeId);

            // Assert
            resultado.IsSuccess.Should().BeFalse();
            repositorioAtividade.DidNotReceive().Excluir(Arg.Any<Atividade>());
            await contextoPersistencia.DidNotReceive().GravarAsync();
        }
    }
}
