using System.ComponentModel.DataAnnotations;

namespace TodoList.Dtos;

public class UserLoginDto
{
    [Required]
    [RegularExpression("^[a-z]+$", ErrorMessage = "Username must consist only of lowercase letters.")]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters long.")]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}