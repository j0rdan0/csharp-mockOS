using static System.Console;

namespace MockOS {


interface IProcess {
    int Stop();
    int GetPid();
    Task<int> Start();
    int GetPriority();
    int SetPriority(int prio);
    

};

interface IThread {
    int GetTid();
    int Run();


};

public class Process: IProcess {

    private static int ProcessCounter;

    private int _exitCode;
    private int _pid;
    private string? _image;
    private int _prio;

    private string[]? _arguments;

    PROC_STATE _state;

    private int Pid { 
        get => _pid;
        set => _pid = value;
    }

    private string? Image {
        get => _image;
        set => _image = value;
    }

    private PROC_STATE Status {
        get => _state;
        set => _state = value;
    }

    private string[]? Args {
        get => _arguments;
        set => _arguments = value;
    }

    private int ExitCode {
        get => _exitCode;
        set => _exitCode = ExitCode;
    }
    private int Priority {
        get => _prio;
        set => _prio = value;

    }
    public Process(string image,string[]? arguments) {
            Image = image;
            Pid = ProcessCounter++;
            Status = PROC_STATE.Sleeping;
            Args = arguments;
            

    }

    private void TerminateProcess(int Pid,out int exitCode) {
            WriteLine($"stopped process with Pid {Pid}");
            ProcessCounter--;
       // return default exit code;
            exitCode = 1;
    }
    public int GetPid() {
        return Pid;
    }

    public int Stop() {
            int exitCode;
            TerminateProcess(Pid,out exitCode);
            return exitCode;
    }
    public  async Task<int> Start() {
           
            Task t = AddToRunQueue();
            WriteLine($"process {Pid} has now status {Status}");
            await t;
            // do some work;
            return Pid;
    }
        private async Task AddToRunQueue() {
             Status = PROC_STATE.Running;
             await Task.Delay(300); // simulate some shit 
            WriteLine("added process {Pid} to run queue");
        }
    public int SetPriority(int prio) {
        if (prio == Priority) {
            return 1; // no changes needed etc
        }
        else {
            Priority = prio;
             return 1; 
        }
       
    }
    public int GetPriority() => Priority;
}



enum PROC_STATE
{
    Running, 
    Sleeping,
    Zombie
};


}