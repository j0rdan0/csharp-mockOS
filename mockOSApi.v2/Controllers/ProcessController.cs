using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;
using Microsoft.Extensions.Localization;

/// <summary>
/// Controller responsible with Process handling
/// </summary>
[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class ProcessController : Controller
{
    private readonly ILogger<ProcessController> _logger;

    private readonly IStringLocalizer<ErrorMessage> _localizer;
    private readonly IProcessService _processService;

    private readonly IAuthenticationService _authorizationService;

    public ProcessController(ILogger<ProcessController> logger, IProcessService processHandler, IAuthenticationService authorizationService,IStringLocalizer<ErrorMessage> localizer)
    {
        _logger = logger;
        _processService = processHandler;
        _authorizationService = authorizationService;
        _localizer = localizer;
    }

    private string? FetchTokenFromHeaders()
    {
        string token;
        try
        {
            token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[0] != "Bearer") {
                return null;
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            return null;
        }
        
        return token;
    }

    // GET /api/process
    [HttpGet]
    public string Index()
    {
        _logger.LogInformation("TBD [{0}]", DateTime.Now);
        if (!_authorizationService.IsUserAuthorized(FetchTokenFromHeaders()))
            return "not authorized";
        else
            return "authorized";
        //  to implement method for returning a random process from proc list

    }

    // GET /api/process/all

    /// <summary>
    /// Get all processes
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public ActionResult<List<MockProcessDto>> GetAll()
    {
        _logger.LogInformation("[*] Processes QUERIED [{0}]", DateTime.Now);
        if (_processService.GetAllProcesses != null)
        {
            return Ok(_processService.GetAllProcesses.ToList());
        }
        else
        {
            return NoContent();
        }

    }

    // GET api/process/[pid]

    /// <summary>
    /// Get process with PID
    /// </summary>
    /// <param name="pid">Process ID</param>
    /// <returns></returns>
    [HttpGet("{pid:int}")]
    public ActionResult<MockProcessDto> Get(int pid)
    {
        var proc = _processService.GetProcessDtoByPid(pid);
        if (proc == null)
        {
            _logger.LogInformation("[*] Process with PID {0} NOT FOUND [{1}]", pid, DateTime.Now);
            return NotFound();
        }
        _logger.LogInformation("[*] Process with PID {0} RETURNED SUCCESSFULLY [{1}]", pid, DateTime.Now);
        return Ok(proc);
    }

    // get api/process/[name]

    /// <summary>
    /// Get process(es) by name
    /// </summary>
    /// <param name="name">Process cmdline</param>
    /// <returns></returns>
    [HttpGet("{name}")]
    public ActionResult<List<MockProcessDto?>> GetProcessByName(string name)
    {
        var procs = _processService.GetProcessByName(name);
        if (procs == null)
        {
            _logger.LogInformation("[*] Process named {0} NOT FOUND [{1}]", name, DateTime.Now);
            return NotFound();
        }
        _logger.LogInformation("[*] Process named {0} RETURNED SUCCESSFULLY [{1}]", name, DateTime.Now);
        return Ok(procs);

    }
    [HttpGet("user/{user}")]
    public ActionResult<List<MockProcessDto?>> GetProcessByUser(string user)
    {
        var procs = _processService.GetProcessByUser(user);
        if (procs == null)
        {
            _logger.LogInformation("[*] Processes running under user {0} NOT FOUND [{1}]", user, DateTime.Now);
            return NotFound();
        }
        _logger.LogInformation("[*] Processes running under {0} RETURNED SUCCESSFULLY [{1}]", user, DateTime.Now);
        return Ok(procs);

    }

    // POST api/process

    /// <summary>
    /// Create a new process
    /// </summary>
    /// <param name="process">ProcessCreation DTO object</param>
    /// <returns></returns>
    /// 
    [HttpPost]
    public async Task<ActionResult<MockProcessDto>> CreateProcess(MockProcessCreationDto process)
    {
        var token = FetchTokenFromHeaders();
        if (!_authorizationService.IsUserAuthorized(token))
            return Unauthorized();
        var user = _authorizationService.GetUser(token);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var proc = await _processService.CreateProcess(process, user);
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

    // DELETE api/process/[pid]

    /// <summary>
    /// Delete an existing process
    /// </summary>
    /// <param name="pid">Process ID</param>
    /// <returns></returns>

    [HttpDelete("{pid:int}")]
    // [HttpGet("delete/{pid:int}")]
    public ActionResult KillProcess(int pid)
    {
        // need to send a NotFound if process is null
        _processService.KillProcess(pid);
        return NoContent();
    }

    // PUT api/process/[priority]

    /// <summary>
    /// Change a process priority
    /// </summary>
    /// <param name="prio">Process priority</param>
    /// <returns></returns>
    [HttpPut("priority")]
    public ActionResult<MockProcessDto> ChangePriority([FromBody] PriorityClass prio)
    {
        var proc = _processService.ChangePriority(prio.Prio, prio.Pid);
        if (proc == null)
        {
            _logger.LogInformation("[*] Process with PID {0} either DOES NOT EXIST or PRIO {1} already set [{2}]", prio.Pid, prio.Prio, DateTime.Now);
            return NotFound();
        }
        _logger.LogInformation("[*] Process with PID {0} CHANGED PRIO to {1} [{2}]", prio.Pid, prio.Prio, DateTime.Now);
        return NoContent();

    } // this is not compliant with PUT method as I should include both MockProcessDto and updates in the body, otherwise use PATCH

    [HttpDelete("all")]
    public ActionResult KillAllProcess()
    {
        _processService.KillAllProcess();
        return NoContent();
    }
}

