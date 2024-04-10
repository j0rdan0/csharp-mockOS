using mockOSApi.Models;
using mockOSApi.Repository;

namespace mockOSApi.DTO;

public class PriorityClass()
{
    public int Prio { get; set; }
    public int Pid { get; set; }
}

public class ProcessDto()
{
    public int Pid { get; set; }
    public string? Image { get; set; }

    public PROC_STATE State { get; set; }

    public int Priority { get; set; }

}

public class ProcessCreationDto()
{

    public int Pid { get; set; }
    public string? Image { get; set; }

    public string[]? Arguments { get; set; }

    public int Priority { get; set; }

}


public interface IMapper
{
    public ProcessDto MapToDTO(MockProcess proc);
    public MockProcess? MapFromDTO(ProcessDto proc);
    public MockProcess MapFromDTO(ProcessCreationDto proc);
}
public class ProcessMapper : IMapper
{
    private readonly IProcessRepository _repository;

    public ProcessMapper(IProcessRepository repository)
    {
        _repository = repository;
    }
    public ProcessDto MapToDTO(MockProcess proc)
    {
        return new ProcessDto
        {
            Pid = proc.Pid,
            Image = proc.Image,
            State = proc.Status,
            Priority = proc.Priority,
        };
    }

    public MockProcess MapFromDTO(ProcessCreationDto proc)
    {
        return new MockProcess
        {
            Pid = proc.Pid,
            Image = proc.Image,
            Priority = proc.Priority,
            Args = proc.Arguments,
        };
    }

    public MockProcess? MapFromDTO(ProcessDto proc)
    {

        // make checks for pid if among existing values, if not return null directly
        return _repository.GetProcessByPid(proc.Pid);
    }
}
