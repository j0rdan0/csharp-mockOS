using mockOSApi.Repository;

namespace mockOSApi.Services;

public interface IAuthentication {
    public Task<bool> AuthenticateUser(string username,string password);
}

public class AuthenticationService: IAuthentication
{
    private readonly IUserRepositoryKv _userRepositoryKv;

    public AuthenticationService(IUserRepositoryKv userRepositoryKv) {
        _userRepositoryKv = userRepositoryKv;
    }

    public async Task<bool> AuthenticateUser(string username,string password) {
        if(!_userRepositoryKv.UserExists(username)) {
            return false;
        }
        var user = await _userRepositoryKv.GetUser(username);
        
        return user.Password == password;
    }

}
