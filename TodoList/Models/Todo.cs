using System.ComponentModel.DataAnnotations;

namespace TodoList.Models;

public class Todo
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Done { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}