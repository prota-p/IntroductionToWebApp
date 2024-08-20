using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoListApp.Data;

namespace TodoListApp.Models;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(10, ErrorMessage = "Title must be less than 10 characters.")]
    public string Title { get; set; } = string.Empty;
    
    [ForeignKey(nameof(ApplicationUser))]
    public string UserId { get; set; } = string.Empty;
}
