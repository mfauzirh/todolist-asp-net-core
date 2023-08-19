using System.ComponentModel.DataAnnotations;
using TodoList.Models;

namespace TodoList.Dtos;

public class UserGetDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [RegularExpression("^[a-z]+$", ErrorMessage = "Username must consist only of lowercase letters.")]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters long.")]
    public string UserName { get; set; } = null!;

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public List<Todo> Todos { get; set; } = new();
}