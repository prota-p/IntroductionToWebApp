using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using TodoListApp.Models;

namespace TodoListApp.Services;

public class TodoListService
{
    private readonly ApplicationDbContext _context;

    public TodoListService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> GetTodosAsync()
    {
        var items = await _context.TodoItems.ToListAsync();
        return items;
    }
}