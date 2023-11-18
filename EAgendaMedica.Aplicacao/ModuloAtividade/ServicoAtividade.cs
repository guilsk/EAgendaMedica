using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
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

            foreach(Medico m in atividade.Medicos) {
                m.Atividades.Add(atividade);
            }

            await contextoPersistencia.GravarAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<Atividade>> EditarAsync(Atividade atividade) {

            var resultadoValidacao = ValidarAtividade(atividade);

            if (resultadoValidacao.IsFailed) return Result.Fail(resultadoValidacao.Errors);

            repositorioAtividade.Editar(atividade);

            foreach (Medico m in atividade.Medicos) {
                if(!m.Atividades.Contains(atividade)) {
                    m.Atividades.Add(atividade);
                }
            }

            await contextoPersistencia.GravarAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<Atividade>> ExcluirAsync(Guid id) {

            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            if (atividade == null) return Result.Fail($"Atividade {id} não encontrada");
            

            repositorioAtividade.Excluir(atividade);

            foreach (Medico m in atividade.Medicos) {
                if (m.Atividades.Contains(atividade)) {
                    m.Atividades.Remove(atividade);
                }
            }

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

            bool conflitoDeHorario = validator.ConfirmarSeAtividadeTemConflitoDeHorario(atividade);

            List<Error> errors = new List<Error>();

            if(conflitoDeHorario) errors.Add(new Error("Esta atividade tem conflito de horário com outra atividade desse mesmo médico."));

            foreach (var error in resultadoValidacao.Errors) {
                errors.Add(new Error(error.ErrorMessage));
            }

            if (errors.Any()) return Result.Fail(errors.ToArray());

            return Result.Ok();
        }
    }
}
