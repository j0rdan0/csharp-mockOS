using MediatR;
using mockOSApi.Models;
using mockOSApi.Requests;

namespace mockOSApi.Handlers;

public class CreateThreadHandler: IRequestHandler<CreateThreadRequest,MockThread> {
    public async Task<MockThread> Handle(CreateThreadRequest request,CancellationToken cancellationToken) {
        
        return new MockThread();
    } 
}