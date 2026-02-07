using MyApi.Api.Models;

namespace MyApi.Api.Interfaces;

public interface ITokenService
{
    string CreateAccessToken(User user);
}
