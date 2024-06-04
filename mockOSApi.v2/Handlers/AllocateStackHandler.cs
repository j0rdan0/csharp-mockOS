using MediatR;
using mockOSApi.Models;
using mockOSApi.Requests;

namespace mockOSApi.Handlers;


public class AllocateStackHandler: IRequestHandler<AllocateStackRequest>{

    private readonly IMemoryAllocatorService _allocator;

    public AllocateStackHandler(IMemoryAllocatorService allocatorService) {
        _allocator = allocatorService;
    }
    public Task Handle(AllocateStackRequest request,CancellationToken cancellationToken) {

        return Task.FromResult(_allocator.CreateStack(request.Thread));
    }
}