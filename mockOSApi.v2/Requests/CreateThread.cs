using mockOSApi.Models;
using MediatR;
using mockOSApi.DTO;

namespace mockOSApi.Requests;

public class CreateThreadRequest: IRequest<MockThreadDto> {
    public string? Name { get; set; }
    public string? StartFunction { get; set; }
    public int ParentPid {get;set;}

}