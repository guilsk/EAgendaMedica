using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.Infra.Orm.Compartilhado;
using EAgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EAgendaMedica.TestesIntegracao.Infra.ModuloMedico {
    [TestClass]
    public class RepositorioMedicoTests : IDisposable {
        private EAgendaDbContext _db;

        private RepositorioMedicoOrm repositorio;

        private IDbContextTransaction transaction;

        [TestInitialize]
        public void Init() {
            var builder = new DbContextOptionsBuilder<EAgendaDbContext>().UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=EAgendaMedicaTeste;Integrated Security=True");

            _db = new EAgendaDbContext(builder.Options);
            repositorio = new RepositorioMedicoOrm(_db);

            transaction = _db.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup() {
            transaction.Rollback();
            transaction.Dispose();

            _db.Dispose();
        }

        [TestMethod]
        public void Deve_Inserir_Medico() {
            //Arrange
            Medico novoMedico = new Medico { Nome = "Dr. Teste", Crm = "01010_AZ"};

            //Action
            repositorio.InserirAsync(novoMedico);
            _db.SaveChanges();

            //Assert
            Medico medicoEncontrado = repositorio.SelecionarPorId(novoMedico.Id);

            Assert.IsNotNull(medicoEncontrado);
            Assert.AreEqual(novoMedico.Id, medicoEncontrado.Id);
            Assert.AreEqual(novoMedico.Nome, medicoEncontrado.Nome);
            Assert.AreEqual(novoMedico.Crm, medicoEncontrado.Crm);
            Assert.AreEqual(novoMedico.Atividades, medicoEncontrado.Atividades);
        }

        [TestMethod]
        public void Deve_Editar_Medico() {
            //Arrange
            Medico novoMedico = new Medico { Nome = "Dr. Teste", Crm = "01010_AZ" };
            repositorio.InserirAsync(novoMedico);
            _db.SaveChanges();

            Medico medicoAtualizado = repositorio.SelecionarPorId(novoMedico.Id);
            medicoAtualizado.Nome = "Dr. Teste II";

            //Action
            repositorio.Editar(novoMedico);
            _db.SaveChanges();

            //Assert

            Assert.IsNotNull(medicoAtualizado);
            Assert.AreEqual(novoMedico.Id, medicoAtualizado.Id);
            Assert.AreEqual(novoMedico.Nome, medicoAtualizado.Nome);
            Assert.AreEqual(novoMedico.Crm, medicoAtualizado.Crm);
            Assert.AreEqual(novoMedico.Atividades, medicoAtualizado.Atividades);
        }
        
        [TestMethod]
        public void Deve_Excluir_Medico() {
            //Arrange
            Medico novoMedico = new Medico { Nome = "Dr. Teste", Crm = "01010_AZ" };
            repositorio.InserirAsync(novoMedico);
            _db.SaveChanges();

            //Action
            repositorio.Excluir(novoMedico);
            _db.SaveChanges();

            //Assert
            Medico medicoEncontrado = repositorio.SelecionarPorId(novoMedico.Id);

            Assert.IsNull(medicoEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Medico() {
            //Arrange
            Medico novoMedico = new Medico { Nome = "Dr. Teste", Crm = "01010_AZ" };
            repositorio.InserirAsync(novoMedico);
            _db.SaveChanges();

            //Action
            Medico medicoEncontrado = repositorio.SelecionarPorId(novoMedico.Id);

            //Assert
            Assert.IsNotNull(medicoEncontrado);
            Assert.AreEqual(novoMedico.Id, medicoEncontrado.Id);
            Assert.AreEqual(novoMedico.Nome, medicoEncontrado.Nome);
            Assert.AreEqual(novoMedico.Crm, medicoEncontrado.Crm);
            Assert.AreEqual(novoMedico.Atividades, medicoEncontrado.Atividades);
        }

        public void Dispose() {
            _db.Dispose();
        }
    }

}
