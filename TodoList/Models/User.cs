using System.ComponentModel.DataAnnotations;

namespace TodoList.Models;

public class User
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

    [Required]
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public List<Todo> Todos { get; set; } = new();
}