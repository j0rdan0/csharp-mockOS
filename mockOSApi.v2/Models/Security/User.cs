using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models;

public record User
{
    private Role[] roles;

    [Key]
    public int Uid { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Role[] Roles { get => roles; set => roles = value; }

};


public enum Role
{
    Administrator,
    User,
    Superuser
};
