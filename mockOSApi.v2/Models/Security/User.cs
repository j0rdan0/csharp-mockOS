using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models;

public record User
{
    [Key]
    public int Uid { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Roles Role { get; set; }

};


public enum Roles
{
    Administrator,
    User,
    Superuser
};
