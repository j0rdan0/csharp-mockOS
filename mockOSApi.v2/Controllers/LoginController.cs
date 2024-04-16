using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;
using mockOSApi.Models;
using System.Collections.Immutable;


[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class LoginController: Controller {

    private readonly ILogger<ProcessController> _logger;
    private readonly IAuthentication _authentication;

    public LoginController(ILogger<ProcessController> logger,IAuthentication authentication) {
        _logger = logger;
        _authentication = authentication;
    }

    // GET api/login/{username:password}
    [HttpGet("{credential}")]
    public async Task<ActionResult> Index(string credential)
    {
       var parts = credential.Split(':');
       var user = parts[0];
       var pass = parts[1];

      var authenticated = await _authentication.AuthenticateUser(user, pass);
      if (authenticated) {
        return Ok();
      }
      return Unauthorized();
    }
}