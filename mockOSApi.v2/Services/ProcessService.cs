using mockOSApi.Repository;
using mockOSApi.Models;
using mockOSApi.Errors;
using mockOSApi.DTO;
using AutoMapper;

namespace mockOSApi.Services;

public interface IProcessService
{
    public IEnumerable<MockProcessDto> AllProcesses { get; }
    public MockProcessDto GetProcessDtoByPid(int pid);
    public MockProcess? GetProcessByPid(int pid);
    public Task<MockProcessDto>? CreateProcess(MockProcessCreationDto proc, User user);
    void KillProcess(int pid); // should return a Process object ?
    public MockProcessDto? ChangePriority(int prio, int pid);
    public void KillAllProcess();
    public List<MockProcessDto>? GetProcessByName(string name);
}

public class ProcessService : IProcessService
{
    public readonly IProcessRepository _repository;
    public readonly IMapper _mapper;

    public ProcessService(IProcessRepository procRepo, IMapper autoMapper)
    {
        _repository = procRepo;
        _mapper = autoMapper;
    }

    public IEnumerable<MockProcessDto>? AllProcesses
    {
#pragma warning disable CS8766 
        get
#pragma warning restore CS8766 

        {
            if (_repository.GetAll().ToList().Count == 0)
            {
                return null;
            }
            List<MockProcessDto> DtoList = new List<MockProcessDto>();
            foreach (var proc in _repository.GetAll().ToList())
            {
                DtoList.Add(_mapper.Map<MockProcessDto>(proc));
            }
            return DtoList;

        }
    }

    public MockProcessDto GetProcessDtoByPid(int pid) => _mapper.Map<MockProcessDto>(_repository.GetProcessByPid(pid));
    public MockProcess? GetProcessByPid(int pid) => _repository.ProcessExists(pid) ? _repository.GetProcessByPid(pid) : null;

    public List<MockProcessDto>? GetProcessByName(string name)
    {
        List<MockProcessDto>? procs = new List<MockProcessDto>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        foreach (var proc in _repository.GetProcessByName(name))
        {
            procs.Add(_mapper.Map<MockProcessDto>(proc));

        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        if (procs.Count == 0)
        {
            return null;
        }
        return procs;

    }
    public async Task<MockProcessDto>? CreateProcess(MockProcessCreationDto process, User user)
    {
        var proc = _mapper.Map<MockProcess>(process);
        //here I need to handle:
        // - setting PID automaticall, starting with 2, as PID should be reserved for init
        // - allocating memory
        // - creating main pool, etc
        // - associated loged in user with current process


        // setting Priority to default unless otherwise value is provided already in DTO
        if (proc.Priority == null)
        {
            proc.Priority = MockProcess.DEFAULT_PRIORITY;
        }

        // setting PID for process;
        var count = _repository.GetProcessCount();

        if (!_repository.ProcessExists(count))
        {
            proc.Pid = ++count;
        }
        else
        {
            var newPid = _repository.GetHigestPid();
            proc.Pid = ++newPid;
        }

        //setting process state to SLEEPING

        proc.Status = ProcessStatus.SLEEPING;

        //setting process exit code to 0, only to be changed if errors when exiting/creating appear

        proc.ExitCode = 0;

        // opening the default file descriptors (stdin, stdout, stderr)
        proc.FileDescriptors = new List<int>();
        foreach (var fd in Enum.GetValues(typeof(DefaultFd)))
        {
            if (proc.FileDescriptors != null)
                proc.FileDescriptors.Add((int)fd);
        }


        // setting IsService status

        if (proc.Image == null)
        {
            proc.IsService = true; // System processes do not export image path to usermode
        }

        // checking image 

        if (!Path.Exists(proc.Image) && !proc.IsService)
        {
            proc.ErrorMessage = new ErrorMessages().ErrorMessage["PATH_ERROR"];
            Console.WriteLine(new ErrorMessages().ErrorMessage["PATH_ERROR"]);
        }


        // TBD allocating memory - heap, VA space etc

        // Creating main thread etc

        //setting user account context

        proc.User = user;



        //setting create time

        proc.CreationTime = DateTime.Now;

        await _repository.CreateProcess(proc);

        return _mapper.Map<MockProcess, MockProcessDto>(proc);
    }

    public void KillProcess(int pid)
    {

        MockProcess? proc = GetProcessByPid(pid);
        if (proc == null)
        {
            return;
        }
        _repository.KillProcess(proc);

    }
    public void KillAllProcess()
    {
        _repository.KillAllProcess();
    }

    public MockProcessDto? ChangePriority(int prio, int pid)
    {
        if (prio == MockProcess.DEFAULT_PRIORITY)
        { // default priority
            return null;
        }
        if (prio > 90)
        {
            prio = 90; // max priority
        }

        var proc = _repository.ChangePriority(prio, pid);
        if (proc == null)
        {
            return null;
        }
        return _mapper.Map<MockProcessDto>(proc);

    }

}