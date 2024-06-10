using mockOSApi.Models;

namespace mockOSApi.DTO;

public class MockThreadDto
{
    public int Tid { get; set; }
    public string? StartFunction { get; set; }
    public ThreadStatus Status { get; set; }
    public string? Name { get; set; }
    public string? ErrorMessage { get; set; }
}