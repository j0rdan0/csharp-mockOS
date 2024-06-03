using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models;

public record User
{
    [Key]
    public int Uid { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Role[] Roles { get; set; }

};


public enum Role
{
    Administrator,
    User,
    Superuser
};
