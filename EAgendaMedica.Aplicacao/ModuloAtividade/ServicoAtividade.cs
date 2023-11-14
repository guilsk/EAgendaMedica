using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloAtividade;
using FluentResults;

namespace EAgendaMedica.Aplicacao.ModuloAtividade {
    public class ServicoAtividade {
        private readonly IRepositorioAtividade repositorioAtividade;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoAtividade(IRepositorioAtividade repositorioAtividade, IContextoPersistencia contextoPersistencia) {
            this.repositorioAtividade = repositorioAtividade;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade) {
            
            var resultadoValidacao = ValidarAtividade(atividade);

            if (resultadoValidacao.IsFailed) return Result.Fail(resultadoValidacao.Errors);

            await repositorioAtividade.InserirAsync(atividade);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<Atividade>> EditarAsync(Atividade atividade) {

            var resultadoValidacao = ValidarAtividade(atividade);

            if (resultadoValidacao.IsFailed) return Result.Fail(resultadoValidacao.Errors);

            repositorioAtividade.Editar(atividade);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<Atividade>> ExcluirAsync(Guid id) {

            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            repositorioAtividade.Excluir(atividade);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<List<Atividade>>> SelecionarTodosAsync() {

            var atividades = await repositorioAtividade.SelecionarTodosAsync();

            return Result.Ok(atividades);
        } 

        public async Task<Result<Atividade>> SelecionarPorIdAsync(Guid id) {

            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            return Result.Ok(atividade);
        }

        private Result ValidarAtividade(Atividade atividade) {

            ValidadorAtividade validator = new ValidadorAtividade();

            var resultadoValidacao = validator.Validate(atividade);

            List<Error> errors = new List<Error>();

            foreach (var error in resultadoValidacao.Errors) {
                errors.Add(new Error(error.ErrorMessage));
            }

            if (errors.Any()) return Result.Fail(errors.ToArray());

            return Result.Ok();
        }
    }
}
