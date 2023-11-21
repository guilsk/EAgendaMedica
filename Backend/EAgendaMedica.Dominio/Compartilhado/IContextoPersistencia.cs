namespace EAgendaMedica.Dominio.Compartilhado {
    public interface IContextoPersistencia {
        Task<bool> GravarAsync();
    }
}
