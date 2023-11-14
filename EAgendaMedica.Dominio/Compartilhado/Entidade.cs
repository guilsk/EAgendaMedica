namespace EAgendaMedica.Dominio.Compartilhado {
    public class Entidade {
        public Guid Id { get; set; }

        public Entidade() {
            Id = Guid.NewGuid();
        }
    }
}
