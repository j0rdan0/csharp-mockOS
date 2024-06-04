using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models;

public interface IThread
{
    // TBD if I need this or not
}
public class MockThread : OSObject
{
    [Key]
    public int Tid { get; set; }
    public string? StartFunction { get; set; }
    public ThreadStatus Status { get; set; }
    public string? Name { get; set; } // thread name
    public int ExitCode { get; set; }
    public Stack<byte[]> Stack { get; set; } // needs to be allocated by VM manager, size of 1024 
    public MockProcess Parent { get; set; }

}

public enum ThreadStatus
{
    RUNNING,
    SLEEPING,
    SUSPENDED,

    TERMINATED,
};