using MediatR;
using mockOSApi.Models;

namespace mockOSApi.Requests;

public class AllocateStackRequest: IRequest {
    public MockThread Thread {get;set;}
}