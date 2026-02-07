using System.Collections.Concurrent;
using MyApi.Api.Interfaces;
using MyApi.Api.Models;

namespace MyApi.Api.Services;

public class InMemoryUserStore : IUserStore
{
    private readonly ConcurrentDictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);

    public User? FindByUsername(string username)
        => _users.TryGetValue(username, out var u) ? u : null;

    public void Add(User user)
        => _users[user.Username] = user;
}
