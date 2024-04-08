using System.ComponentModel.DataAnnotations;

using mockOSApi.Data;

namespace mockOSApi.Models;

interface IProcess {
    int Stop();
    int GetPid();
    Task<int> Start();
    int GetPriority();
    int SetPriority(int prio);
    
};

public class Process<T>: OSObject, IProcess {

    private readonly ApplicationDbContext _appDbContext;

    private readonly ILogger<T>? _logger;
    private List<Thread> threadlist = new();

    private  int _processCounter;

    public static int ProcessCounter { // need to replace this with the number of actual rows in Process table;
       get;set;
    }

    private int _exitCode;
    private int _pid;
    private string? _image;
    private int _prio;

    private int[]? _fds;

    private string[]? _arguments;

    PROC_STATE _state;

    [Key]
    public int Pid { 
        get => _pid;
        init => _pid = value;
    }

    public string? Image {
        get => _image;
        set => _image = value;
    }

   
    public PROC_STATE Status {
        get => _state;
        set => _state = value;
    }

    public string[]? Args {
        get => _arguments;
        set => _arguments = value;
    }

    public int ExitCode {
        get => _exitCode;
        set => _exitCode = ExitCode;
    }
    public int Priority {
        get => _prio;
        set => _prio = value;

    }

    private int[]? FileDescriptors {
        get => _fds;
        set => _fds = value;
    }
    public Process(string image,string[]? arguments,ILogger<T>? logger,ApplicationDbContext applicationDbContext) {
            Image = image;
            IncrementProcessCounter(applicationDbContext);
            _appDbContext = applicationDbContext;

           Pid = ProcessCounter; // get PID from DB
           
            Status = PROC_STATE.Sleeping;
            Args = arguments;
            Priority = 50; //default value
            _logger = logger;

            
            
    }

     public Process(string image) {
            Image = image;
             IncrementProcessCounter(_appDbContext);
            Pid = ProcessCounter;
            Status = PROC_STATE.Sleeping;
            Priority = 50; //default value
           
    }
     public Process() {
         IncrementProcessCounter(_appDbContext);
            Pid = ProcessCounter;
            Status = PROC_STATE.Sleeping;
            Priority = 50; //default value , use const instead 
    }



    public static void InitProcessCounter(ApplicationDbContext applicationDbContext) {
        var procCounter = applicationDbContext.ProcessCounters.FirstOrDefault();
        if (procCounter == null) {
            procCounter = new ProcessCounter{Id= 0,ProcCounter=0};
            applicationDbContext.ProcessCounters.Add(procCounter);
            applicationDbContext.SaveChanges();

        }
        else {
            ProcessCounter = procCounter.ProcCounter;
        }

    }

    public static void IncrementProcessCounter(ApplicationDbContext applicationDbContext) {
                 var procCounter = applicationDbContext.ProcessCounters.FirstOrDefault();
                 if(procCounter != null) {
                    procCounter.ProcCounter = procCounter.ProcCounter++;
                    applicationDbContext.SaveChanges();
                    ProcessCounter =  procCounter.ProcCounter++;
                 }
    }
    public static void DecrementProcessCounter(ApplicationDbContext applicationDbContext) {
                 var procCounter = applicationDbContext.ProcessCounters.FirstOrDefault();
                 if(procCounter != null) {
                    procCounter.ProcCounter = procCounter.ProcCounter--;
                    applicationDbContext.SaveChanges();
                    ProcessCounter =  procCounter.ProcCounter--;
                 }
    }
    public static string GetProcNumber() {
         
       return String.Format("proc number is now {0}",ProcessCounter);

    }

   
    internal void TerminateProcess(int Pid,out int exitCode) {
            _logger.LogInformation($"stopped process with Pid {Pid}");
            DecrementProcessCounter(_appDbContext);
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
    public async Task<int> Start() {
           
            Task t = AddToRunQueue();
            _logger.LogInformation($"process {Pid} has now status {Status}");
            await t;
            // do some work;
            return Pid;
    }
        private async Task AddToRunQueue() {
             Status = PROC_STATE.Running;
             await Task.Delay(300); // simulate some shit 
            _logger.LogInformation("added process {Pid} to run queue");
        }
    public int SetPriority(int prio) {
        if (prio == Priority) {
            _logger.LogInformation("priority not changed");
            return 1; // no changes needed etc
        }
        else {
            Priority = prio;
            _logger.LogInformation("priority set to {0}",prio);
             return 1; 
        }
       
    }
    public int GetPriority() => Priority;
}



public enum PROC_STATE
{
    Running, 
    Sleeping,
    Zombie
};

public enum DEFAULT_FD {
    STDIN,
    STDOUT,
    STDERR
};


