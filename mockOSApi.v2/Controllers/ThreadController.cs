using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;
using MediatR;
using mockOSApi.Handlers;
using mockOSApi.Requests;


/// <summary>
/// Controller responsible with Thread handling
/// </summary>
[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class ThreadController: Controller {
    private readonly ILogger<ThreadController> _logger;

    private readonly IThreadService _threadService;

    private readonly IMediator  _mediator;

    public ThreadController(ILogger<ThreadController> logger, IThreadService threadHandler,IMediator mediator)
    {
        _logger = logger;
        _threadService = threadHandler;
        _mediator = mediator;
    }

     [HttpPost]
    public async Task<ActionResult<MockThreadDto>> CreateThread(CreateThreadRequest request)
    {
        var thread = await _mediator.Send(request);

        if (thread == null)
        {
            _logger.LogInformation("[*] Thread could not be CREATED [{0}]", DateTime.Now);
            return BadRequest(thread);
        }
        if (thread.ErrorMessage != string.Empty) {
            _logger.LogInformation("[*] {0} [{1}]",thread.ErrorMessage,DateTime.Now); // this is for when errors are catched, but only logged
        }                                            
        _logger.LogInformation("[*] Thread with TID {0} CREATED [{0}]", thread.Tid, DateTime.Now);

        return Ok(thread);
        
    }





}