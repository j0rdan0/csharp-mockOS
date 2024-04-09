using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using mockOSApi.Models;
using NuGet.Protocol.Plugins;


[ApiController]
[Route("/api/[controller]")]
public class ProcessController: Controller {
     private readonly ILogger<ProcessController> _logger;
     private readonly IProcessHandler _handler;
   
     public ProcessController(ILogger<ProcessController> logger,IProcessHandler processHandler) {
        _logger = logger;
        _handler = processHandler;
 
     }

// GET /process
[HttpGet]
     public string Index() {

      _logger.LogInformation("created process");
        return "created process\n";
     }

// GET /process/all


[HttpGet("all")]
     public ActionResult<List<MockProcess>> GetAll() {
        _logger.LogInformation("got processes");
   
        return _handler.GetAllProcesses().ToList();
     }

[HttpGet("{pid:int}")]
     public ActionResult<MockProcess> Get(int pid) {
        return _handler.GetProcessByPid(pid);
     }





}