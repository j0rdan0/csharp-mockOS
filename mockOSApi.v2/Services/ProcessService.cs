using mockOSApi.Repository;
using mockOSApi.Models;
using mockOSApi.DTO;
using AutoMapper;

namespace mockOSApi.Services;

public interface IProcessService
{
    public IEnumerable<MockProcessDto> AllProcesses { get; }

    public MockProcessDto GetProcessByPid(int pid);
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

    public MockProcessDto GetProcessByPid(int pid) => _mapper.Map<MockProcessDto>(_repository.GetProcessByPid(pid));

    public async Task CreateProcess(MockProcessCreationDto process)
    {

        await _repository.CreateProcess(_mapper.Map<MockProcess>(process));
        await _repository.Save();
    }

    public void KillProcess(int pid)
    {

        _repository.KillProcess(pid);

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