using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Model;

namespace ToDoListApi.Repositories;

public class TarefaRepository
{
    private readonly ApplicationDbContext _context;

    public TarefaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tarefa>> ListarTodasTarefas()
    {
        return await _context.Tarefas.ToListAsync();
    }

    public async Task<Tarefa?> ObterTarefaPorId(int id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task<Tarefa> CriarTarefa(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task AtualizarTarefa(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarTarefa(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa != null)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
