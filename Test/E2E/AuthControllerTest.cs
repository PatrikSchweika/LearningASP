using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using Data.Repositories;

using LearningASP.DTO.Auth;
using LearningASP.Model;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Xunit.Abstractions;

namespace LearningASPTest.E2E;

public class AuthControllerTest
{
    private readonly HttpClient _client;

    private readonly RegisterDto _registerDto = new()
    {
        Email = "travolta@email.com",
        Password = "mypassword",
        FirstName = "John",
        LastName = "Travolta"
    };

    public AuthControllerTest()
    {
        var factory = new LearningAspWebApplicationFactory();

        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IUserRepository, ListUserRepository>();
                });
            })
            .CreateClient();
    }

    [Fact]
    public async Task ShouldRegisterUser()
    {
        var response = await _client.PostAsJsonAsync("/auth/register", _registerDto);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ShouldNotRegisterUserWithSameEmail()
    {
        var response1 = await _client.PostAsJsonAsync("/auth/register", _registerDto);

        response1.EnsureSuccessStatusCode();

        var response2 = await _client.PostAsJsonAsync("/auth/register", _registerDto);

        Assert.Equal(HttpStatusCode.Conflict, response2.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnCurrentUserAfterLogin()
    {
        var registerResponse = await _client.PostAsJsonAsync("/auth/register", _registerDto);

        registerResponse.EnsureSuccessStatusCode();

        var loginDto = new LoginDto { Email = _registerDto.Email, Password = _registerDto.Password };

        var loginResponse = await _client.PostAsJsonAsync("/auth/login", loginDto);

        loginResponse.EnsureSuccessStatusCode();

        var token = await loginResponse.Content.ReadAsStringAsync();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var currentUserResponse = await _client.GetAsync("/auth/user");

        currentUserResponse.EnsureSuccessStatusCode();

        var currentUser = await currentUserResponse.Content.ReadFromJsonAsync<User>();

        Assert.NotNull(currentUser);
        Assert.Equal(currentUser.Email, _registerDto.Email);
        Assert.Equal(currentUser.FirstName, _registerDto.FirstName);
        Assert.Equal(currentUser.LastName, _registerDto.LastName);
    }
}