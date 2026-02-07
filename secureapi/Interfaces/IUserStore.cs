using MyApi.Api.Models;

namespace MyApi.Api.Interfaces;

public interface IUserStore
{
    User? FindByUsername(string username);
    void Add(User user);
}
