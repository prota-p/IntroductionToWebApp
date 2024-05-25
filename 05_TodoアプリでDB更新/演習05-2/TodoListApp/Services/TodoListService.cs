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
        var todoItems = await _context.TodoItems.ToListAsync();
        return todoItems;
    }

    public async Task UpdateTodoAsync(TodoItem todoItemInput)
    {
        var existingTodoItem = await _context.TodoItems.FindAsync(todoItemInput.Id);
        if (existingTodoItem != null)
        {
            existingTodoItem.Title = todoItemInput.Title;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTodoAsync(int id)
    {
        var existingTodoItem = await _context.TodoItems.FindAsync(id);
        if (existingTodoItem != null)
        {
            _context.TodoItems.Remove(existingTodoItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddTodoAsync(TodoItem newTodoItem)
    {
        _context.TodoItems.Add(newTodoItem);
        await _context.SaveChangesAsync();
    }
}