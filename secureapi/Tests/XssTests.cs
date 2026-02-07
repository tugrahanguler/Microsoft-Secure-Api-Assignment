using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MyApi.Tests;

public class XssTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public XssTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Notes_rejects_html_like_input()
    {
        var token = await LoginAndGetToken("alice", "User123!");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var res = await _client.PostAsJsonAsync("/api/notes", new { content = "<script>alert(1)</script>" });
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private async Task<string> LoginAndGetToken(string username, string password)
    {
        var res = await _client.PostAsJsonAsync("/api/auth/login", new { username, password });
        res.EnsureSuccessStatusCode();

        var body = await res.Content.ReadFromJsonAsync<LoginResponse>();
        return body!.accessToken;
    }

    private sealed class LoginResponse
    {
        public string accessToken { get; set; } = default!;
        public string tokenType { get; set; } = default!;
    }
}
