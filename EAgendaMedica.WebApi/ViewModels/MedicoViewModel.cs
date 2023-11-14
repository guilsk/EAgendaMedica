namespace EAgendaMedica.WebApi.ViewModels {
    public class InserirMedicoViewModel {
        public string Nome { get; set; }
        public string Crm { get; set; }
    }

    public class EditarMedicoViewModel {
        public string Nome { get; set; }
        public string Crm { get; set; }
    }

    public class ListarMedicoViewModel {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Crm { get; set; }
    }

    public class VisualizarMedicoViewModel {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Crm { get; set; }
        public List<ListarAtividadeViewModel> Atividades { get; set; }
    }
}
