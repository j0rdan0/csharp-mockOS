using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;
using mockOSApi.Models;
using System.Collections.Immutable;
using Microsoft.IdentityModel.Tokens;


[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class LoginController : Controller
{

    private readonly ILogger<ProcessController> _logger;
    private readonly IAuthentication _authentication;

    public LoginController(ILogger<ProcessController> logger, IAuthentication authentication)
    {
        _logger = logger;
        _authentication = authentication;
    }

    // GET api/login/{username:password}
    [HttpGet("{credential}")]
    public async Task<ActionResult<string>> Index(string credential)
    {
        var parts = credential.Split(':');
        var user = parts[0];
        var pass = parts[1];

        var authenticatedUser = await _authentication.AuthenticateUser(user, pass);
        if (authenticatedUser == null)
        {
            return Unauthorized();

        }
        var tokenString = _authentication.GenerateToken(authenticatedUser);
        return Ok(new { token = tokenString });
    }
}