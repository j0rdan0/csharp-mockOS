
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using mockOSApi.Data;
using mockOSApi.Models;
using System.Text.Json;

namespace mockOSApi.Controllers;

public class TestController: Controller {

    private readonly ILogger<TestController> _logger;
    private readonly ApplicationDbContext _appDbContext;

    public TestController(ILogger<TestController> logger,ApplicationDbContext appDbContext) {
        _logger = logger;
        _appDbContext = appDbContext;
        Process<TestController>.InitProcessCounter(_appDbContext);  
        
    }

    public IActionResult Index() {

        mockOSApi.Models.Process<TestController> p = new Process<TestController>("/usr/bin/tru",null,_logger,_appDbContext);
        _appDbContext.Add(p);
        _appDbContext.SaveChanges();

        ViewData.Add("process",JsonSerializer.Serialize(p));

        return View();
         
    }
    public string Something() {
        var t = _appDbContext.Add(new mockOSApi.Models.Thread() {Tid=1});
        _appDbContext.SaveChanges();
       
        var tInfo = _appDbContext.Threads
            .First();

        
        _logger.LogInformation("hit something at {0}",DateTime.Now);
        return String.Format("got TID: {0}\n",tInfo.Tid.ToString());
    }

    public string DeleteProcess(int pid) {
        var pInfo = _appDbContext.Processes 
        .Where (p => p.Pid == pid)
        .ToList();
        ;


        foreach (var p in pInfo) {
            try {
                _logger.LogInformation($"got {p.GetPid()}");
                _appDbContext.Remove(p);
               
               // 
                 return String.Format("removed process with pid {0}",pid);
            }
            catch(Exception e) {
                _logger.LogError(e.Message);
                return String.Format("err: {0}\n",e.Message);
            }
            finally {
                 _appDbContext.SaveChanges(true);
            }
               
            
            
        }
        return String.Format("failed removing process with pid {0}",pid);
       
    }

    public string GetProcNo() {

        return String.Format("{0}\n",Process<TestController>.GetProcNumber());
    }
}