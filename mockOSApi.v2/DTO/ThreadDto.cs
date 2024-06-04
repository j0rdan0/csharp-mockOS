using mockOSApi.Models;

namespace mockOSApi.DTO;

public class MockThreadCreationDto {
    public int Tid {get;set;}
    public string? StartFunction { get; set; }

    public ThreadStatus Status { get; set; }
    public string? Name { get; set; }
    public int ParentPid {get;set;}
}

public class MockThreadDto {

    public int Tid {get;set;}
    public string? StartFunction { get; set; }

    public ThreadStatus Status { get; set; }
    public string? Name { get; set; }


}