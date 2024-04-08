using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.Models;


namespace mockOSApi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) // so some first injection shit, ILogger being injected
    {
        _logger = logger;
    }

        // so GET /Home
    public IActionResult Index()
    {
     

        return View();
    }

            // so GET /Home/Privacy
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Test() {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
