using MyApi.Api.Interfaces;
using MyApi.Api.Models;

namespace MyApi.Api.Services;

public class AuthService
{
    private readonly IUserStore _users;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenService _tokens;

    public AuthService(IUserStore users, IPasswordHasher hasher, ITokenService tokens)
    {
        _users = users;
        _hasher = hasher;
        _tokens = tokens;
    }

    public (bool ok, string? token) Login(string username, string password)
    {
        var user = _users.FindByUsername(username);
        if (user is null) return (false, null);

        if (!_hasher.Verify(password, user.PasswordHash)) return (false, null);

        var token = _tokens.CreateAccessToken(user);
        return (true, token);
    }
}
