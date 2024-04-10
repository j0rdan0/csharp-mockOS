using System.ComponentModel.DataAnnotations;


namespace mockOSApi.Models;


public class MockProcess : OSObject
{

    /*
    Fields and properties
    ****************************
    */

    private int _processCounter;

    public static int ProcessCounter
    { // need to replace this with the number of actual rows in Process table;
        get; set;
    }
    private int _exitCode;
    private int _pid;
    private string? _image;
    private int _prio;
    private int[]? _fds;
    private string[]? _arguments;
    PROC_STATE _state;

    [Key]
    public int Pid
    {
        get => _pid;
        init => _pid = value;
    }
    public string? Image
    {
        get => _image;
        set => _image = value;
    }


    public PROC_STATE Status
    {
        get => _state;
        set => _state = value;
    }

    public string[]? Args
    {
        get => _arguments;
        set => _arguments = value;
    }

    public int ExitCode
    {
        get => _exitCode;
        set => _exitCode = ExitCode;
    }
    public int Priority
    {
        get => _prio;
        set => _prio = value;

    }

    private int[]? FileDescriptors
    {
        get => _fds;
        set => _fds = value;
    }

    /*
    Constructor members
    ********************************
    */
    public MockProcess(string image, string[]? arguments)
    {
        Image = image;

        // get PID from DB

        Status = PROC_STATE.Sleeping;
        Args = arguments;
        Priority = 50; //default value
    }

    public MockProcess(string image)
    {
        Image = image;

        Status = PROC_STATE.Sleeping;
        Priority = 50; //default value

    }
    public MockProcess()
    {
        Status = PROC_STATE.Sleeping;
        Priority = 50; //default value , use const instead 
    }


}

public enum PROC_STATE
{
    Running,
    Sleeping,
    Zombie
};

public enum DEFAULT_FD
{
    STDIN,
    STDOUT,
    STDERR
};


public interface IProcess
{

}