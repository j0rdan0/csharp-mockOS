using mockOSApi.Models;
using MediatR;

namespace mockOSApi.Requests;

public class CreateThreadRequest: IRequest<MockThread> {
    public string? Name { get; set; }
    public string? StartFunction { get; set; }
    public MockProcess Parent {get;set;}

}