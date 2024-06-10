using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;


/// <summary>
/// Controller responsible with Thread handling
/// </summary>
[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class ThreadController: Controller {
    private readonly ILogger<ThreadController> _logger;

    private readonly IThreadService _threadService;

    public ThreadController(ILogger<ThreadController> logger, IThreadService processHandler)
    {
        _logger = logger;
        _threadService = processHandler;
    }

     [HttpPost]
    public async Task<ActionResult<MockThreadDto>> CreateProcess(MockThreadCreationDto thread)
    {
        var t = await _threadService.CreateThread(thread);

        if (t == null)
        {
            _logger.LogInformation("[*] Thread could not be CREATED [{0}]", DateTime.Now);
            return BadRequest(thread);
        }
        if( t.ErrorMessage != string.Empty) {
            _logger.LogInformation("[*] {0} [{1}]",t.ErrorMessage,DateTime.Now); // this is for when errors are catched, but only logged
        }                                            
        _logger.LogInformation("[*] Thread with TID {0} CREATED [{0}]", t.Tid, DateTime.Now);

        return Ok(t);
    }





}