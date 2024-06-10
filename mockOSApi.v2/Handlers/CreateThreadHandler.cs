using MediatR;
using mockOSApi.DTO;
using mockOSApi.Requests;
using mockOSApi.Services;

namespace mockOSApi.Handlers;

public class CreateThreadHandler : IRequestHandler<CreateThreadRequest, MockThreadDto>
{
    private readonly IThreadService _threadService;
    public CreateThreadHandler(IThreadService threadService)
    {
        _threadService = threadService;
    }
    public async Task<MockThreadDto> Handle(CreateThreadRequest request, CancellationToken cancellationToken) => await _threadService.CreateThread(request);
}