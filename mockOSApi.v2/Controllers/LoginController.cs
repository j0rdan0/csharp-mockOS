using mockOSApi.Services;
using Microsoft.AspNetCore.Mvc;
using mockOSApi.DTO;

/// <summary>
/// Controller responsible with user authentication
/// </summary>
[ApiController]
[Route("/api/[controller]")]
[Produces("application/json")]
public class LoginController : Controller
{
    private readonly ILogger<ProcessController> _logger;
    private readonly IAuthenticationService _authentication;

    public LoginController(ILogger<ProcessController> logger, IAuthenticationService authentication)
    {
        _logger = logger;
        _authentication = authentication;
    }

    /// <summary>
    /// Login an existing user
    /// </summary>
    /// <param name="user">A username and a password member; anonymous access is allowed</param>
    /// <returns>A JWT token if user authenticated successfully</returns>
    [HttpPost]
    public async Task<ActionResult<string>> Login(UserCreationDTO user)
    {
        var username = user.Username.ToLower();
        var pass = user.Password;
        var authenticatedUser = await _authentication.AuthenticateUser(username, pass);
        if (authenticatedUser == null)
        {
            return Unauthorized();

        }
        var tokenString = _authentication.GenerateToken(authenticatedUser);
        return Ok(new { token = tokenString });
    }

    /// <summary>
    /// Register a new user to the API
    /// </summary>
    /// <param name="user">A UserCreationDTO that takes at least a username and password</param>
    /// <returns>The newly created user</returns>
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(UserCreationDTO user)
    {
        var username = user.Username.ToLower();
        var pass = user.Password; // to check if password meets requirements 

        if (pass == null)
        {
            _logger.LogInformation("[*] Empty password not allowed [{0}]", DateTime.Now);
            return BadRequest(user);
        }
        if (username == "anonymous")
        {
            _logger.LogInformation("[*] Reserved username anonymous not allowed [{0}]", DateTime.Now);
            return BadRequest(user);
        }
        var newUser = await _authentication.RegisterUser(user);
        if (newUser == null)
        {
            return BadRequest(user);
        }
        return newUser;

    }
}