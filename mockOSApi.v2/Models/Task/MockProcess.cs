using System.ComponentModel.DataAnnotations;

namespace mockOSApi.Models;

public class MockProcess : OSObject
{

     public const int DEFAULT_PRIORITY = 50;
    [Key]
    public int Pid { get; init; }
    public int Id { get;set;}
    public ProcessCounter Counter { get;set;}
    public string? Image { get; set; }
    public ProcState Status { get; set; }
    public string[]? Args { get; set; }
    public int ExitCode { get; set; }
    public int? Priority { get; set; }
    private int[]? FileDescriptors { get; set; }

    public MockProcess(string image, string[]? arguments)
    {
        Image = image;

        // get PID from DB


        Status = ProcState.SLEEPING; // should avoid changing this here !!
        Args = arguments;
        Priority = DEFAULT_PRIORITY;
        ExitCode = 0;
    }

    public MockProcess(string image)
    {
        Image = image;
      //  Pid = Counter.Counter++;
        Status = ProcState.SLEEPING;
        Priority = DEFAULT_PRIORITY;

    }
    public MockProcess()
    {
       // Pid = Counter.Counter++;
        Status = ProcState.SLEEPING;
        Priority = DEFAULT_PRIORITY;
    }


}

public enum ProcState
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

public class ProcessCounter { 
    public static int Value { get; set;}
    public int Pid;
}