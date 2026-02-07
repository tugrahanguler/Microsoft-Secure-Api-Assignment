namespace MyApi.Api.Dtos;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string AccessToken, string TokenType = "Bearer");
