using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using mockOSApi.Models;
using NuGet.Protocol.Plugins;
using mockOSApi.DTO;




[ApiController]
[Route("/api/[controller]")]
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


    [HttpGet("all")]
    public ActionResult<List<MockProcessDto>> GetAll()
    {
        _logger.LogInformation("got processes");

        return Ok(_processService.AllProcesses.ToList());
    }

    // GET /process/[pid]
    [HttpGet("{pid:int}")]
    public ActionResult<MockProcessDto> Get(int pid)
    {
        return Ok(_processService.GetProcessByPid(pid));
    }

    [HttpPost]
    public async Task<ActionResult> CreateProcess(MockProcessCreationDto process)
    {
        _logger.LogInformation("created process");
        await _processService.CreateProcess(process);
        return Ok(process);
    }

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