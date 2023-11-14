using AutoMapper;
using EAgendaMedica.Aplicacao.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EAgendaMedica.WebApi.Controllers {

    [Route("api/atividades")]
    [ApiController]
    public class AtividadeController : ControllerBase{
        private readonly ServicoAtividade servicoAtividade;
        private readonly IMapper mapeador;

        public AtividadeController(ServicoAtividade servicoAtividade, IMapper mapeador)
        {
            this.servicoAtividade = servicoAtividade;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ListarAtividadeViewModel>), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos(){

            var atividadesResult = await servicoAtividade.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarAtividadeViewModel>>(atividadesResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualisacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarAtividadeViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id) {

            var atividadeResult = await servicoAtividade.SelecionarPorIdAsync(id);

            if (atividadeResult.IsFailed) return NotFound(atividadeResult.Errors);

            var viewModel = mapeador.Map<VisualizarAtividadeViewModel>(atividadeResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirAtividadeViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirAtividadeViewModel viewModel) {

            var atividade = mapeador.Map<Atividade>(viewModel);

            var atividadeResult = await servicoAtividade.InserirAsync(atividade);

            if (atividadeResult.IsFailed) return BadRequest(atividadeResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarAtividadeViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarAtividadeViewModel viewModel) {

            var selecaoAtividadeResult = await servicoAtividade.SelecionarPorIdAsync(id);

            if (selecaoAtividadeResult.IsFailed) return NotFound(selecaoAtividadeResult.Errors);

            var atividade = mapeador.Map(viewModel, selecaoAtividadeResult.Value);

            var atividadeResult = await servicoAtividade.EditarAsync(atividade);

            if (atividadeResult.IsFailed) return BadRequest(atividadeResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id) {
            
            var selecaoAtividadeResult = await servicoAtividade.SelecionarPorIdAsync(id);

            if (selecaoAtividadeResult.IsFailed) return NotFound(selecaoAtividadeResult.Errors);

            await servicoAtividade.ExcluirAsync(id);

            return Ok();
        }
    }

}