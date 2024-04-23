using System.ComponentModel.DataAnnotations;
using mockOSApi.Models;

namespace mockOSApi.DTO;

public class PriorityClass()
{
    public int Prio { get; set; }
    public int Pid { get; set; }
}

public class MockProcessDto()
{
    public int Pid { get; set; }
    public string? Image { get; set; }
    public ProcessStatus Status { get; set; }
    public int Priority { get; set; }
    public List<int> FileDescriptors { get; set; }
    public bool? IsService {get;set;}
    public string? ErrorMessage { get; set; }
    public User User {get;set;}
}

public class MockProcessCreationDto()
{
    public string? Image { get; set; }
    public string[]? Args { get; set; }
    public int? Priority { get; set; }
    public bool? IsService { get;set; }
}

