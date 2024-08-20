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
        try
        {
            var items = await _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();
            return items;
        }
        catch (Exception ex)
        {
            throw new TodoListServiceException("Failed to retrieve todo items.", ex);
        }
    }

    public async Task UpdateTodoAsync(string userId, TodoItem todoItemInput)
    {
        try
        {
            var existingTodoItem = await _context.TodoItems.FindAsync(todoItemInput.Id);
            if (existingTodoItem == null || existingTodoItem.UserId != userId)
            {
                throw new TodoListServiceException("Todo item not found or access denied.");
            }
            existingTodoItem.Title = todoItemInput.Title;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TodoListServiceException("Failed to update todo item.", ex);
        }
    }

    public async Task DeleteTodoAsync(string userId, int todoId)
    {
        try
        {
            var existingTodoItem = await _context.TodoItems.FindAsync(todoId);
            if (existingTodoItem == null || existingTodoItem.UserId != userId)
            {
                throw new TodoListServiceException("Todo item not found or access denied.");
            }
            _context.TodoItems.Remove(existingTodoItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TodoListServiceException("Failed to delete todo item.", ex);
        }
    }

    public async Task AddTodoAsync(string userId, TodoItem newTodoItem)
    {
        try
        {
            newTodoItem.UserId = userId;
            _context.TodoItems.Add(newTodoItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TodoListServiceException("Failed to add todo item.", ex);
        }
    }
}