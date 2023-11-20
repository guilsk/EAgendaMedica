using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.Infra.Orm.Compartilhado;
using EAgendaMedica.Infra.Orm.ModuloAtividade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EAgendaMedica.TestesIntegracao.Infra.ModuloAtividade {
    [TestClass]
    public class RepositorioAtividadeTests : IDisposable{
        private EAgendaDbContext _db;

        private RepositorioAtividadeOrm repositorio;

        private IDbContextTransaction transaction;

        [TestInitialize] 
        public void Init() {
            var builder = new DbContextOptionsBuilder<EAgendaDbContext>().UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=EAgendaMedicaTeste;Integrated Security=True");

            _db = new EAgendaDbContext(builder.Options);
            repositorio = new RepositorioAtividadeOrm(_db);

            transaction = _db.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup() {
            transaction.Rollback();
            transaction.Dispose();

            _db.Dispose();
        }

        [TestMethod]
        public void Deve_Inserir_Atividade() {
            //Arrange
            Atividade novaAtividade = new Atividade { Descricao = "Atividade Teste", Data = DateTime.Now, HoraInicio = TimeSpan.Zero, HoraFim = TimeSpan.Zero, TipoAtividade = TipoAtividadeEnum.Consulta, Medicos = new List<Medico>() { new Medico { Nome = "Dr. Teste", Crm = "12345-AZ" } } };


            //Action
            repositorio.InserirAsync(novaAtividade);
            _db.SaveChanges();

            //Assert
            Atividade atividadeEncontrada = repositorio.SelecionarPorId(novaAtividade.Id);

            Assert.IsNotNull(atividadeEncontrada);
            Assert.AreEqual(novaAtividade.Id, atividadeEncontrada.Id);
            Assert.AreEqual(novaAtividade.Descricao, atividadeEncontrada.Descricao);
            Assert.AreEqual(novaAtividade.Data, atividadeEncontrada.Data);
            Assert.AreEqual(novaAtividade.HoraInicio, atividadeEncontrada.HoraInicio);
            Assert.AreEqual(novaAtividade.HoraFim, atividadeEncontrada.HoraFim);
            Assert.AreEqual(novaAtividade.TipoAtividade, atividadeEncontrada.TipoAtividade);
            Assert.AreEqual(novaAtividade.Medicos, atividadeEncontrada.Medicos);
        }

        [TestMethod]
        public void Deve_Editar_Atividade() {
            //Arrange
            Atividade novaAtividade = new Atividade { Descricao = "Atividade Teste", Data = DateTime.Now, HoraInicio = TimeSpan.Zero, HoraFim = TimeSpan.Zero, TipoAtividade = TipoAtividadeEnum.Consulta, Medicos = new List<Medico>() { new Medico { Nome = "Dr. Teste", Crm = "12345-AZ" } } };
            repositorio.InserirAsync(novaAtividade);
            _db.SaveChanges();

            Atividade atividadeAtualizada = repositorio.SelecionarPorId(novaAtividade.Id);
            atividadeAtualizada.Descricao = "Teste Atividade";

            //Action
            repositorio.Editar(atividadeAtualizada);
            _db.SaveChanges();

            //Assert
            Assert.IsNotNull(atividadeAtualizada);
            Assert.AreEqual(novaAtividade.Id, atividadeAtualizada.Id);
            Assert.AreEqual(novaAtividade.Descricao, atividadeAtualizada.Descricao);
            Assert.AreEqual(novaAtividade.Data, atividadeAtualizada.Data);
            Assert.AreEqual(novaAtividade.HoraInicio, atividadeAtualizada.HoraInicio);
            Assert.AreEqual(novaAtividade.HoraFim, atividadeAtualizada.HoraFim);
            Assert.AreEqual(novaAtividade.TipoAtividade, atividadeAtualizada.TipoAtividade);
            Assert.AreEqual(novaAtividade.Medicos, atividadeAtualizada.Medicos);
        }

        [TestMethod]
        public void Deve_Excluir_Atividade() {
            //Arrange
            Atividade novaAtividade = new Atividade { Descricao = "Atividade Teste", Data = DateTime.Now, HoraInicio = TimeSpan.Zero, HoraFim = TimeSpan.Zero, TipoAtividade = TipoAtividadeEnum.Consulta, Medicos = new List<Medico>() { new Medico { Nome = "Dr. Teste", Crm = "12345-AZ" } } };
            repositorio.InserirAsync(novaAtividade);
            _db.SaveChanges();

            //Action
            repositorio.Excluir(novaAtividade);
            _db.SaveChanges();

            //Assert
            Atividade atividadeEncontrada = repositorio.SelecionarPorId(novaAtividade.Id);
            
            Assert.IsNull(atividadeEncontrada);
        }

        [TestMethod]
        public void Deve_Selecionar_Atividade() {
            //Arrange
            Atividade novaAtividade = new Atividade { Descricao = "Atividade Teste", Data = DateTime.Now, HoraInicio = TimeSpan.Zero, HoraFim = TimeSpan.Zero, TipoAtividade = TipoAtividadeEnum.Consulta, Medicos = new List<Medico>() { new Medico { Nome = "Dr. Teste", Crm = "12345-AZ" } } };
            repositorio.InserirAsync(novaAtividade);
            _db.SaveChanges();

            //Action
            Atividade atividadeEncontrada = repositorio.SelecionarPorId(novaAtividade.Id);

            //Assert
            Assert.IsNotNull(atividadeEncontrada);
            Assert.AreEqual(novaAtividade.Id, atividadeEncontrada.Id);
            Assert.AreEqual(novaAtividade.Descricao, atividadeEncontrada.Descricao);
            Assert.AreEqual(novaAtividade.Data, atividadeEncontrada.Data);
            Assert.AreEqual(novaAtividade.HoraInicio, atividadeEncontrada.HoraInicio);
            Assert.AreEqual(novaAtividade.HoraFim, atividadeEncontrada.HoraFim);
            Assert.AreEqual(novaAtividade.TipoAtividade, atividadeEncontrada.TipoAtividade);
            Assert.AreEqual(novaAtividade.Medicos, atividadeEncontrada.Medicos);
        }

        public void Dispose() {
            _db.Dispose();
        }
    }

}
