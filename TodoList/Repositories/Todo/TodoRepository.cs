using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly DataContext _context;

    public TodoRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Todo> Add(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> Delete(Todo todo)
    {
        _context.Remove(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo?> GetById(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<IEnumerable<Todo>> GetByUserId(int userId)
    {
        return await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Todo> Update(Todo todo)
    {
        _context.Update(todo);
        await _context.SaveChangesAsync();
        return todo;
    }
}