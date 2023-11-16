using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EAgendaMedica.TestesUnitarios.Domínio.ModuloMedico
{
    [TestClass]
    public class MedicoTest
    {

        private Medico medico;
        private ValidadorMedico validadorMedico;
        private ValidationResult resultado;

        [TestInitialize]
        public void Init()
        {
            medico = new Medico();
            validadorMedico = new ValidadorMedico();
            resultado = validadorMedico.Validate(medico);
        }

        [TestMethod]
        public void Não_deve_aceitar_campo_nome_e_crm_vazio()
        {
            resultado.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Deve_verificar_o_CRM()
        {
            medico.Nome = "Kylliam";
            medico.Crm = "123";
            resultado = validadorMedico.Validate(medico);
            resultado.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Deve_aceitar_lista_atividades_null()
        {
            medico.Atividades.Should().BeNull();
        }

        [TestMethod]
        public void Deve_aceitar_lista_atividades_vazia()
        {
            medico.Atividades = new List<Atividade>();
            medico.Atividades.Should().BeEmpty();
        }

    }
}
