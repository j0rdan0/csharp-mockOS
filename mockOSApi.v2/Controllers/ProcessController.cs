using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;




[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class ProcessController : Controller
{
    private readonly ILogger<ProcessController> _logger;
    private readonly IProcessService _processService;

    public ProcessController(ILogger<ProcessController> logger, IProcessService processHandler)
    {
        _logger = logger;
        _processService = processHandler;

    }

    // GET /api/process
    [HttpGet]
    public string Index()
    {

        _logger.LogInformation("created process");
        return "created process\n";
    }

    // GET /api/process/all

    /// <summary>
    /// Get all processes
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public ActionResult<List<MockProcessDto>> GetAll()
    {
        _logger.LogInformation("got processes");

        return Ok(_processService.AllProcesses.ToList());
    }

    // GET /process/[pid]
    /// <summary>
    /// Get process with PID
    /// </summary>
    /// <param name="pid">Process ID</param>
    /// <returns></returns>
    [HttpGet("{pid:int}")]
    public ActionResult<MockProcessDto> Get(int pid)
    {
        return Ok(_processService.GetProcessDtoByPid(pid));
    }
/// <summary>
/// Create a new process
/// </summary>
/// <param name="process">ProcessCreation DTO object</param>
/// <returns></returns>
    [HttpPost]
    
    public async Task<ActionResult> CreateProcess(MockProcessCreationDto process)
    {
        _logger.LogInformation("created process");
        await _processService.CreateProcess(process);
        return Ok(process);
    }
/// <summary>
/// Delete an existing process
/// </summary>
/// <param name="pid">Process ID</param>
/// <returns></returns>
    [HttpGet("delete/{pid:int}")]
    public ActionResult KillProcess(int pid)
    {
        _processService.KillProcess(pid);
        return Ok();
    }

    [HttpPut("priority")]
    public ActionResult ChangePriority([FromBody] PriorityClass prio)
    {
        _processService.ChangePriority(prio.Prio, prio.Pid);
        return Ok();

    }

}

