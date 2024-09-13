using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using Data.Repositories;
using LearningASP.DTO;
using LearningASP.Model;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit.Abstractions;

namespace LearningASPTest.E2E;

public class UserControllerTest
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly CreateUserDto _createUserDto = new()
    {
        FirstName = "Johny",
        LastName = "Smith"
    };

    public UserControllerTest(ITestOutputHelper testOutputHelper)
    {
        var factory = new LearningAspWebApplicationFactory();

        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Replace database repository with list repository
                    services.RemoveAll<IUserRepository>();
                    services.AddScoped<IUserRepository, ListUserRepository>();
                });
            })
            .CreateClient();
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnEmptyList()
    {
        var response = await _client.GetAsync("/user");

        response.EnsureSuccessStatusCode(); //200-299

        var users = await response.Content.ReadFromJsonAsync<User[]>();

        Assert.NotNull(users);
        Assert.Empty(users);
    }

    [Fact]
    public async Task PostNewUser_ShouldReturnNewUser()
    {
        var user = await AddUser(_createUserDto);

        Assert.NotNull(user);
        Assert.Equal(_createUserDto.FirstName, user.FirstName);
        Assert.Equal(_createUserDto.LastName, user.LastName);

        _testOutputHelper.WriteLine($"New user id: ${user.Id}");
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnOneUser()
    {
        await AddUser(_createUserDto);

        var response = await _client.GetAsync("/user");

        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<User[]>();

        Assert.NotNull(users);
        Assert.Single(users);
    }

    [Fact]
    public async Task DeleteUser_ShouldDeleteUser()
    {
        var user = await AddUser(_createUserDto);

        var response = await _client.DeleteAsync($"/user/{user.Id}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser()
    {
        var user = await AddUser(_createUserDto);

        var response = await _client.GetAsync($"/user/{user.Id}");

        response.EnsureSuccessStatusCode();

        var fetchedUser = await response.Content.ReadFromJsonAsync<User>();

        Assert.Equivalent(user, fetchedUser);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync($"/user/1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ReplaceUserById_ShouldReplaceUser()
    {
        var user = await AddUser(_createUserDto);

        var editedUser = new PutUserDto
        {
            Id = user.Id,
            FirstName = "Phillipe",
            LastName = "Eckhart"
        };

        var response = await _client.PutAsJsonAsync($"/user", editedUser);

        response.EnsureSuccessStatusCode();

        var responseUser = await response.Content.ReadFromJsonAsync<User>();

        Assert.Equivalent(editedUser, responseUser);
    }

    [Fact]
    public async Task PatchUserById_ShouldPatchUser()
    {
        var user = await AddUser(_createUserDto);

        var patchUser = new PatchUserDto
        {
            FirstName = "Dickie"
        };

        var response = await _client.PatchAsJsonAsync($"/user/{user.Id}", patchUser);

        response.EnsureSuccessStatusCode();

        var responseUser = await response.Content.ReadFromJsonAsync<User>();

        Assert.NotNull(responseUser);
        Assert.Equal(responseUser.Id, user.Id);
        Assert.Equal(responseUser.FirstName, patchUser.FirstName);
        Assert.Equal(responseUser.LastName, user.LastName);
    }

    private async Task<User?> AddUser(CreateUserDto createUserDto)
    {
        var response = await _client.PostAsJsonAsync("/user", createUserDto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<User>();
    }
}