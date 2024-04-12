using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using mockOSApi.Models;
using mockOSApi.Repository;

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

    public ProcState Status { get; set; }

    public int Priority { get; set; }

}

public class MockProcessCreationDto()
{

    [Required]
    public int Pid { get; set; }
    public string? Image { get; set; }

    public string[]? Args { get; set; }

    public int Priority { get; set; }

}

