using System.ComponentModel.DataAnnotations;

namespace TodoList.Dtos;

public class TodoUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Done { get; set; } = false;
}