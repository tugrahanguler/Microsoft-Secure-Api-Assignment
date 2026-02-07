using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MyApi.Tests;

public class AuthRbacTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthRbacTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_with_invalid_password_returns_401()
    {
        var res = await _client.PostAsJsonAsync("/api/auth/login", new { username = "alice", password = "wrong" });
        res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Admin_dashboard_requires_admin_role()
    {
        // login as normal user
        var login = await _client.PostAsJsonAsync("/api/auth/login", new { username = "alice", password = "User123!" });
        login.EnsureSuccessStatusCode();
        var body = await login.Content.ReadFromJsonAsync<LoginResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body!.accessToken);

        var res = await _client.GetAsync("/api/admin/dashboard");
        res.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Admin_can_access_admin_dashboard()
    {
        var login = await _client.PostAsJsonAsync("/api/auth/login", new { username = "admin", password = "Admin123!" });
        login.EnsureSuccessStatusCode();
        var body = await login.Content.ReadFromJsonAsync<LoginResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body!.accessToken);

        var res = await _client.GetAsync("/api/admin/dashboard");
        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private sealed class LoginResponse
    {
        public string accessToken { get; set; } = default!;
        public string tokenType { get; set; } = default!;
    }
}
