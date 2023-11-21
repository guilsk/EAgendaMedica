using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EAgendaMedica.ConsoleApp {
    internal class Program {
        static void Main(string[] args) {
            var novoMedico = new Medico();
            novoMedico.Nome = "Dr. Teste I";
            novoMedico.Crm = "00000-AA";
            var novoMedico2 = new Medico();
            novoMedico2.Nome = "Dr. Teste II";
            novoMedico2.Crm = "09876-ZZ";
            var novoMedico3 = new Medico();
            novoMedico3.Nome = "Dr. Teste III";
            novoMedico3.Crm = "12345-AZ";

            var novaAtividade = new Atividade();
            novaAtividade.Descricao = "Cirurgia de um certo paciente ae que tá com uma garrafa nas costas";
            novaAtividade.Data = DateTime.Now;
            novaAtividade.HoraInicio = TimeSpan.Parse("13:30:00");
            novaAtividade.HoraFim = TimeSpan.Parse("17:30:00");
            novaAtividade.TipoAtividade = TipoAtividadeEnum.Cirurgia;
            novaAtividade.Medicos.Add(novoMedico);
            novaAtividade.Medicos.Add(novoMedico3);


            var optionsBuilder = new DbContextOptionsBuilder<EAgendaDbContext>();

            IConfiguration configuracao = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=EAgendaMedica;Integrated Security=True");

            var dbContext = new EAgendaDbContext(optionsBuilder.Options);

            dbContext.Add(novoMedico);
            dbContext.Add(novoMedico2);
            dbContext.Add(novoMedico3);

            dbContext.SaveChanges();
        }
    }
}