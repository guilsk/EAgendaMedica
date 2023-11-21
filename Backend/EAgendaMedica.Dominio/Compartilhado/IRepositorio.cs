namespace EAgendaMedica.Dominio.Compartilhado {
    public interface IRepositorio<TEntidade> where TEntidade : Entidade {
        Task<bool> InserirAsync(TEntidade novoRegistro);
        void Editar(TEntidade registro);
        void Excluir(TEntidade registro);

        TEntidade SelecionarPorId(Guid id);

        Task<TEntidade> SelecionarPorIdAsync(Guid id);
        Task<List<TEntidade>> SelecionarTodosAsync();
    }
}
