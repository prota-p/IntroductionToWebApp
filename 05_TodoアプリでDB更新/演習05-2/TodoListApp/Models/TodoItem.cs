using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
