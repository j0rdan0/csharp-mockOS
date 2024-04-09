using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using mockOSApi.Models;
using NuGet.Protocol.Plugins;
using mockOSApi.DTO;
using System.Xml;


[ApiController]
[Route("/api/[controller]")]
public class ProcessController: Controller {
     private readonly ILogger<ProcessController> _logger;
     private readonly IProcessHandler _handler;
   
     public ProcessController(ILogger<ProcessController> logger,IProcessHandler processHandler) {
        _logger = logger;
        _handler = processHandler;
 
     }

// GET /api/process
[HttpGet]
     public string Index() {

      _logger.LogInformation("created process");
        return "created process\n";
     }

// GET /api/process/all


[HttpGet("all")]
     public ActionResult<List<MockProcess>> GetAll() {
        _logger.LogInformation("got processes");
   
        return Ok(_handler.AllProcesses.ToList());
     }

// GET /process/[pid]
[HttpGet("{pid:int}")]
     public ActionResult<MockProcess> Get(int pid) {
        return Ok(_handler.GetProcessByPid(pid));
     }

[HttpPost]
public ActionResult CreateProcess(MockProcess process) {
      _logger.LogInformation("created process with pid: {0}",process.Pid);
    _handler.CreateProcess(process);
    return Ok();
}

[HttpGet("delete/{pid:int}")]
public ActionResult KillProcess(int pid) {
   var p = _handler.GetProcessByPid(pid);
   _handler.KillProcess(p);
   return Ok(p);
}

[HttpPut("priority")] 
public ActionResult ChangePriority([FromBody] PriorityClass prio) {
    
   
   _handler.ChangePriority(prio.Prio,prio.Pid);
   return Ok();

}

}