using mockOSApi.Models;
using Azure.Security.KeyVault.Secrets;

namespace mockOSApi.Repository;

public interface IUserRepositoryKv
{
    public Task<User?> GetUser(string username);
    public Task<User> CreateUser(string username, string password);
    public Task<User> UpdatePassword(string username, string password);
    public Task DeleteUser(string username);
    public bool UserExists(string username);
}


public class UserRepository : IUserRepositoryKv
{

    private readonly SecretClient _secretClient;

    public UserRepository(SecretClient secretClient)
    {
        _secretClient = secretClient;
    }


    public async Task<User?> GetUser(string username)
    {
        var resp = await _secretClient.GetSecretAsync(username);
        if (resp == null)
        {
            return null;
        }
        var user = new User
        {
            Username = username,
            Password = resp.Value.Value
        };
        return user;
    }

    public async Task<User> CreateUser(string username, string password)
    {
        var resp = await _secretClient.SetSecretAsync(username, password);
        var user = new User
        {
            Username = username,
            Password = password
        };
        return user;
    }
    public async Task<User> UpdatePassword(string username, string password)
    {
        var resp = await _secretClient.SetSecretAsync(username, password);
        var user = new User
        {
            Username = username,
            Password = password
        };
        return user;
    }
    public async Task DeleteUser(string username) => await _secretClient.StartDeleteSecretAsync(username);

    public bool UserExists(string username)
    {
        var users = _secretClient.GetPropertiesOfSecrets();
        foreach (var user in users)
        {
            if (username.ToLower() == user.Name)
            {
                return true;
            }
        }
        return false;
    }

}



