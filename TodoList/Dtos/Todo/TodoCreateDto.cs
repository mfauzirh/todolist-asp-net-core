using System.ComponentModel.DataAnnotations;

namespace TodoList.Dtos;

public class TodoCreateDto
{

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}