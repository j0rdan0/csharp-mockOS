using mockOSApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

public class ProcessController: Controller {
     private readonly ILogger<ProcessController> _logger;
     public readonly IProcessRepository _processrepo;

     public ProcessController(ILogger<ProcessController> logger,IProcessRepository procrepo) {
        _logger = logger;
        _processrepo = procrepo;
     }

// GET /process
     public string Index() {

       Task t = _processrepo.CreateProcess();
       Task t2 = _processrepo.Save();

       Task.WaitAll(t,t2);
        return "created process\n";
     }

// GET /process/all
     public string All() {
   
        var payload = new StringBuilder();
        foreach(var proc in _processrepo.GetAll()) {
            payload.Append(JsonSerializer.Serialize(proc));
        }
        return payload.ToString();
     }





}