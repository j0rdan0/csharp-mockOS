using mockOSApi.Models;
using System.ComponentModel.DataAnnotations;

namespace mockOSApi.DTO;

public class UserCreationDTO
{

    [Required]
    public string Username { get; set; }
    public string? Password { get; set; }

    public Role? Role { get; set; }
}

public class UserDTO
{
    public string Username { get; set; }
    public Role Role { get; set; }
}
