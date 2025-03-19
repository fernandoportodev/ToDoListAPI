using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Model;
using ToDoListApi.Model.DTO;
using ToDoListApi.Repositories;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaRepository _repository;

        public TarefaController(TarefaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodasTarefas()
        {
            var tarefas = await _repository.ListarTodasTarefas();
            return Ok(tarefas);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarTarefa([FromBody] TarefaDTO dto)
        {
            var tarefa = new Tarefa
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Status = dto.Status,
                DataCriacao = dto.DataCriacao,
                DataConclusao = dto.DataConclusao

            };
            var novaTarefa = await _repository.CriarTarefa(tarefa);
            return CreatedAtAction(nameof(ObterTarefaPorId), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTarefaPorId(int id)
        {
            var tarefa = await _repository.ObterTarefaPorId(id);
            if (tarefa == null) return NotFound("Tarefa não encontrada");
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Tarefa))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] TarefaDTO dto)
        {
            var tarefa = new Tarefa
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Status = dto.Status,
                DataCriacao = dto.DataCriacao,
                DataConclusao = dto.DataConclusao

            };

            var tarefaExistente = await _repository.ObterTarefaPorId(id);
            if (tarefaExistente == null) return NotFound("Tarefa não encontrada");

            tarefaExistente.Nome = dto.Nome;
            tarefaExistente.Descricao = dto.Descricao;
            tarefaExistente.Status = dto.Status;
            tarefaExistente.DataCriacao = dto.DataCriacao;
            tarefaExistente.DataConclusao = dto.DataConclusao ?? tarefaExistente.DataConclusao;

            await _repository.AtualizarTarefa(tarefaExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefaExistente = await _repository.ObterTarefaPorId(id);
            if (tarefaExistente == null) return NotFound("Tarefa não encontrada");

            await _repository.DeletarTarefa(id);
            return NoContent();

        }
    }
}
