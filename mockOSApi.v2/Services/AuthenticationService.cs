using Microsoft.IdentityModel.Tokens;
using mockOSApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mockOSApi.Models;
using mockOSApi.DTO;
using AutoMapper;

namespace mockOSApi.Services;

public interface IAuthentication
{
    public Task<User?> AuthenticateUser(string username, string password);
    public string GenerateToken(User user);
    public bool IsUserAuthorized(string? tokenString);
    public User? GetUser(string? tokenString);
    public Task<UserDTO?> RegisterUser(UserCreationDTO userTemplate);
}

public class AuthenticationService : IAuthentication
{
    private readonly IUserRepositoryKv _userRepositoryKv;
    private readonly IConfiguration _configuration;

    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepositoryKv userRepositoryKv, IConfiguration configuration, IMapper mapper)
    {
        _userRepositoryKv = userRepositoryKv;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<User?> AuthenticateUser(string username, string password)
    {
        // allow anonymous authentication
        if (username == "anonymous")
        {
            return new User
            {
                Username = username,
                Role = Roles.User
            };
        }
        if (!_userRepositoryKv.UserExists(username))
        {
            return null;
        }
        var user = await _userRepositoryKv.GetUser(username);

        if (user.Password == password)
        {
            return user;
        }
        return null;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["SecretString"]);

        var tokenDescriptor = new SecurityTokenDescriptor // constructing the token descriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
           {
                new Claim(ClaimTypes.Name, user.Username.ToString())
               }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

    public bool IsUserAuthorized(string? tokenString)
    {
        if (tokenString == null)
            return false;
        var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
        var username = token.Claims.FirstOrDefault(c => c.ValueType == ClaimValueTypes.String).Value;
        if (username == null) { return false; }
        // get username from token
        if (token.ValidTo > DateTime.Now)
        { // check if token is not expired
            return false;
        }
        return true;
        // more checks to be performed
    }

    public User? GetUser(string? tokenString)
    {
        // checks should be already made by IsUserAuthorized
        var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
        var username = token.Claims.FirstOrDefault(c => c.ValueType == ClaimValueTypes.String).Value;
        var user = new User
        {
            Username = username,
            Role = Roles.Administrator,
        };
        return user;
    }

    public async Task<UserDTO?> RegisterUser(UserCreationDTO userTemplate)
    {
        var user = _mapper.Map<User>(userTemplate);
        // will implement password policy checks later
        try
        {
            var inUse = await _userRepositoryKv.GetUser(user.Username);
        }
        catch (Azure.RequestFailedException e)
        {
            if (e.ErrorCode == "SecretNotFound")
            { // this is new user indeed
                var newUser = await _userRepositoryKv.CreateUser(user.Username, user.Password);
                return _mapper.Map<UserDTO>(newUser);
            }
            else
            {
                throw;
            }

        }
        return null;

    }

}
