using System.Net.Http.Headers;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/login", new
        {
            Email = email,
            Password = password
        });

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        return result?.Token;
    }
}

public class LoginResponse
{
    public string Token { get; set; }
}