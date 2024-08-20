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

    public async Task<List<TodoItem>> GetTodosAsync(string userId)
    {
        var items = await _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();
        return items;
    }

    public async Task UpdateTodoAsync(string userId, TodoItem todoItemInput)
    {
        var existingTodoItem = await _context.TodoItems.FindAsync(todoItemInput.Id);
        if (existingTodoItem != null && existingTodoItem.UserId == userId)
        {
            existingTodoItem.Title = todoItemInput.Title;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTodoAsync(string userId, int todoId)
    {
        var existingTodoItem = await _context.TodoItems.FindAsync(todoId);
        if (existingTodoItem != null && existingTodoItem.UserId == userId)
        {
            _context.TodoItems.Remove(existingTodoItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddTodoAsync(string userId, TodoItem newTodoItem)
    {
        newTodoItem.UserId = userId;
        _context.TodoItems.Add(newTodoItem);
        await _context.SaveChangesAsync();
    }
}