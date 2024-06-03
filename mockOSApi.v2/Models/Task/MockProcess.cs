using System.ComponentModel.DataAnnotations;


namespace mockOSApi.Models;

public class MockProcess : OSObject
{
    public const int DEFAULT_PRIORITY = 50;

    public const int MAX_PROC = Int32.MaxValue - 1; // max number of processes that can be spawned 

    [Key]
    public int Pid { get; set; }
    public string? Image { get; set; }
    public ProcessStatus Status { get; set; }
    public string[]? Args { get; set; }
    public int ExitCode { get; set; }
    public int? Priority { get; set; }
    public List<int>? FileDescriptors { get; set; }
    public bool IsService { get; set; }

    public TimeSpan RunTime { get; set; }

    public int UserUid { get; set; }

    public User User { get; set; }
    /*
    public MockProcess(string image, string[]? arguments)
    {
    }
    public MockProcess(string image)
    {
    }
    public MockProcess() { }
    */

}

public enum ProcessStatus
{
    RUNNING,
    SLEEPING,
    ZOMBIE,
    DEAD
};

public enum DefaultFd
{
    STDIN,
    STDOUT,
    STDERR
};


public interface IProcess
{
    //TBD
}
