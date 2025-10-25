using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers 
{
	[ApiController]
	[Route("api/[controller]")]
	public class TarefasController : ControllerBase
	{
		private readonly AppDbContext _context; // Injeção de dependência do contexto do banco de dados

		public TarefasController(AppDbContext context)
		{
			_context = context; // Inicializa o contexto do banco de dados
		}

		// GET: api/tarefas
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefa() 
			// Controlador assicrono, Task<ActionResult> => combinação permite executar operações assicronas, libera
			// thread do servidor para outras tarefas.
			// IEnumerable => marca uma classe q deseja implementar, onde possa ser iterável.
		{
			return await _context.Tarefas.ToListAsync();
		}

		// GET: api/tarefas/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Tarefa>> GetTarefa(int id)
		{
			var tarefa = await _context.Tarefas.FindAsync(id); // Cria variavel var. Fornece de forma assicrona chave primaria da entidade Tarefas com base ID
			
			if (tarefa == null)
				return NotFound();
			
			return tarefa;
		}

		// POST: api/tarefas
		[HttpPost]
		public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
		{
			_context.Tarefas.Add(tarefa); // Add novo user no banco, adicionando os campos da entidade
			await _context.SaveChangesAsync(); // forma assicrona salva no banco

            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa); // Cria um novo dado vazio, retorna passando getTarefa e o novo id 
        }

		// PUT: api/tarefas/id
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa) // Recebe os dados de id e tarefa
		{
			if (id != tarefa.Id) // se não existir, da um bad request
				return BadRequest();

			_context.Entry(tarefa).State = EntityState.Modified; // caso exista, recebe aquele objeto de entrada (tarefa) e altera com o State
			await _context.SaveChangesAsync(); // salva

			return Ok("Tarefa alterada com sucesso."); //Retorna uma mensagem Ok()
		}

		// DELETE: api/tarefas/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTarefas(int id)
		{
			var tarefa = await _context.Tarefas.FindAsync(id); // Cria variável tarefa = atribui de forma assicriona o dado do banco passando pelo id na tabela Tarefas
			if (tarefa == null) // Condição para caso o id seja nulo
				return NotFound();

			_context.Tarefas.Remove(tarefa); // Remove aquele determinado dado atribuido do id para a variável
			await _context.SaveChangesAsync(); // Salva as alterações feita

			return Ok("Tarefa deletada com sucesso.");

		}
	}
}
