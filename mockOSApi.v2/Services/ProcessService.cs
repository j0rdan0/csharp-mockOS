using mockOSApi.Repository;
using mockOSApi.Models;
using mockOSApi.DTO;
using AutoMapper;

namespace mockOSApi.Services;

public interface IProcessService
{
    public IEnumerable<MockProcessDto> AllProcesses { get; }

    public MockProcessDto GetProcessDtoByPid(int pid);
    public MockProcess GetProcessByPid(int pid);
    public Task CreateProcess(MockProcessCreationDto proc);
    void KillProcess(int pid);
    public void ChangePriority(int prio, int pid);
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
    public MockProcess? GetProcessByPid(int pid) => _repository.GetProcessByPid(pid);
    public async Task CreateProcess(MockProcessCreationDto process)
    {

        var proc = _mapper.Map<MockProcess>(process);
        //here I need to handle:
        // - setting PID automaticall, starting with 2, as PID should be reserved for init
        // - setting priority to default, unless already set in DTO
        // - setting Status to Sleeping
        // - allocating memory
        // - creating main pool, etc
        await _repository.CreateProcess(proc);
        await _repository.Save();
    }

    public void KillProcess(int pid)
    {

        MockProcess? proc = GetProcessByPid(pid);
        if (proc == null) {
            return;
        }
        _repository.KillProcess(proc);

    }

    public void ChangePriority(int prio, int pid)
    {
        if (prio == MockProcess.DEFAULT_PRIORITY)
        { // default priority
            return;
        }
        if (prio > 90)
        {
            prio = 90; // max priority
        }

        _repository.ChangePriority(prio, pid);

    }
}