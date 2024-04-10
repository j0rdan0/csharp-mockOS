using System.Text.Json;
using mockOSApi.Repository;
using mockOSApi.Models;
using mockOSApi.DTO;

namespace mockOSApi.Services;


public interface IProcessHandler
{
    public IEnumerable<ProcessDto> AllProcesses { get; }

    public ProcessDto GetProcessByPid(int pid);
    public Task CreateProcess(ProcessCreationDto proc);
    public void KillProcess(ProcessDto proc);
    public void ChangePriority(int prio, int pid);
}

public class ProcessHandler : IProcessHandler
{

    public readonly IProcessRepository _repository;
    public readonly IMapper _mapper;

    public ProcessHandler(IProcessRepository procRepo, IMapper mapper)
    {
        _repository = procRepo;
        _mapper = mapper;
    }

    public IEnumerable<ProcessDto>? AllProcesses
    {
#pragma warning disable CS8766 

        get
#pragma warning restore CS8766 

        {
            if (_repository.GetAll().ToList().Count == 0)
            {
                return null;
            }
            List<ProcessDto> DtoList = new List<ProcessDto>();
            foreach (var proc in _repository.GetAll().ToList())
            {
                DtoList.Add(_mapper.MapToDTO(proc));
            }
            return DtoList;

        }
    }

    public ProcessDto GetProcessByPid(int pid) => _mapper.MapToDTO(_repository.GetProcessByPid(pid));

    public async Task CreateProcess(ProcessCreationDto process)
    {

        await _repository.CreateProcess(_mapper.MapFromDTO(process));
        await _repository.Save();
    }

    public void KillProcess(ProcessDto proc)
    {
        _repository.KillProcess(_mapper.MapFromDTO(proc));

    }

    public void ChangePriority(int prio, int pid)
    {
        if (prio == 50)
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