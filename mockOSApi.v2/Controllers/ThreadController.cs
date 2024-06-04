using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;
using Microsoft.Extensions.Localization;


/// <summary>
/// Controller responsible with Thread handling
/// </summary>
[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class ThreadController: Controller {
    private readonly ILogger<ProcessController> _logger;

    private readonly IThreadService _threadService;

    public ThreadController(ILogger<ProcessController> logger, IThreadService processHandler)
    {
        _logger = logger;
        _threadService = processHandler;
    }

     [HttpPost]
    public async Task<ActionResult<MockProcessDto>> CreateProcess(MockThreadCreationDto thread)
    {
        

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var proc = await _threadService.CreateThread(thread);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        if (proc == null)
        {
            _logger.LogInformation("[*] Process could not be CREATED [{0}]", DateTime.Now);
            return BadRequest(process);
        }
        if( proc.ErrorMessage != string.Empty) {
            _logger.LogInformation("[*] {0} [{1}]",proc.ErrorMessage,DateTime.Now); // this is for when image is invalid, this is checked, but since outside of scope,
                                                        // the proc is still created with invalid image, but this is logged
        }
        _logger.LogInformation("[*] Process with PID {0} CREATED [{0}]", proc.Pid, DateTime.Now);

        return CreatedAtAction(nameof(Get), new { pid = proc.Pid }, proc);
    }





}