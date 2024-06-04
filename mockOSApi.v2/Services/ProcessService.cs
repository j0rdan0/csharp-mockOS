using mockOSApi.Repository;
using mockOSApi.Models;
using mockOSApi.DTO;
using AutoMapper;

namespace mockOSApi.Services;

public interface IProcessService
{
    public IEnumerable<MockProcessDto> GetAllProcesses { get; }
    public MockProcessDto GetProcessDtoByPid(int pid);
    public MockProcess? GetProcessByPid(int pid);

    public List<MockProcessDto>? GetProcessByUser(string username);
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

    public readonly IMockProcessBuilder _builder;

    public ProcessService(IProcessRepository procRepo, IMapper autoMapper, IMockProcessBuilder builder)
    {
        _repository = procRepo;
        _mapper = autoMapper;
        _builder = builder;
    }

    public IEnumerable<MockProcessDto>? GetAllProcesses
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

    public List<MockProcessDto>? GetProcessByUser(string username)
    {
        List<MockProcessDto>? procs = new List<MockProcessDto>();
        foreach (var proc in _repository.GetAll().Where(proc => proc.User.Username == username))
        {
            procs.Add(_mapper.Map<MockProcessDto>(proc));
        }
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
       
        // - allocating memory
        // - creating main pool, etc
      

        // TBD allocating memory - heap, VA space etc

        // Creating main thread etc

        var newProc = _builder
        .AddPriority(proc.Priority)
        .AddPid()
        .AddProcessStatus()
        .AddExitCode()
        .AddDefaultFds()
        .AddImage(proc.Image)
        .AddArguments(proc.Args)
        .AddCreationTime()
        .AddUserContext(user)
        .Build();

        await _repository.CreateProcess(newProc);
        return _mapper.Map<MockProcess, MockProcessDto>(newProc);
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