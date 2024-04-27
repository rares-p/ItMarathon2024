using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain;
using Domain.Dto;
using Domain.Entities;
using Services.Contracts;

namespace Services.Services;

public class AdminService : IAdminService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserConfiguration _userConfiguration;

    public AdminService(IHttpClientFactory httpClientFactory, UserConfiguration userConfiguration)
    {
        _httpClientFactory = httpClientFactory;
        _userConfiguration = userConfiguration;
    }

    public async Task<Result<List<UserDto>>> GetAllUsersAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _userConfiguration.Token);
        var getAllUsersResponse = await httpClient.GetAsync("/");    //URL

        if (getAllUsersResponse is null || !getAllUsersResponse.IsSuccessStatusCode)
            return new Result<List<UserDto>>(false, null!, "Am intampinat o eroare");
        List<UserDto>? usersDto = null;
        try
        {
            usersDto = await getAllUsersResponse.Content.ReadFromJsonAsync<List<UserDto>>();
        }
        catch (Exception ex)
        {
            return new Result<List<UserDto>>(false, null!, "Am intampinat o eroare");
        }

        if (usersDto is null)
            return new Result<List<UserDto>>(false, null!, "Am intampinat o eroare");

        return new Result<List<UserDto>>(true, usersDto, "");
    }

    public async Task<Result<User>> CreateUserAsync(User user)
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _userConfiguration.Token);
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("name", user.Name),
            new KeyValuePair<string, string>("identifier", user.Identifier),
            new KeyValuePair<string, string>("credits", user.Credits.ToString()),
            new KeyValuePair<string, string>("grade", user.Grade.ToString(CultureInfo.InvariantCulture))
        });
        var createUserResponse = await httpClient.PostAsync("/", data);

        if (!createUserResponse.IsSuccessStatusCode)
            return new Result<User>(false, null!, "eroare la creearea de user");
        return new Result<User>(true, null!, "");
    }

    public async Task<Result<User>> CreateUserAsync(string identifier)
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _userConfiguration.Token);
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("identifier", identifier)
        });
        var createUserResponse = await httpClient.PostAsync("/", data);

        if (!createUserResponse.IsSuccessStatusCode)
            return new Result<User>(false, null!, "eroare la creearea de user");
        return new Result<User>(true, null!, "");
    }
}