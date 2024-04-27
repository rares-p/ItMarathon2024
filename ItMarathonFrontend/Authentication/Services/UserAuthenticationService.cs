using System.Net;
using System.Net.Http;
using System.Text.Json;
using Authentication.Contracts;
using Domain;
using Domain.Dto;

namespace Authentication.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserConfiguration _userConfiguration;

    public UserAuthenticationService(IHttpClientFactory httpClientFactory, UserConfiguration userConfiguration)
    {
        _httpClientFactory = httpClientFactory;
        _userConfiguration = userConfiguration;
    }

    public async Task<Result<LoginDto>> LogIn(string username, string password)
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
        });
        HttpResponseMessage result;
        try
        {
            result = await httpClient.PostAsync("/users/login", data).ConfigureAwait(false);
        }
        catch(Exception e)
        {
            return new Result<LoginDto>(false, null!, e.Message);
        }

        if (result.StatusCode == HttpStatusCode.NotFound)
            return new Result<LoginDto>(false, null!, "User not found");

        if(!result.IsSuccessStatusCode)
            return new Result<LoginDto>(false, null!, "Unknown error encountered from server");
        File.WriteAllText("C:\\Users\\panai\\Desktop\\test.txt", await result.Content.ReadAsStringAsync());
        LoginDto? response = null;
        try
        {
            response = JsonSerializer.Deserialize<LoginDto>(await result.Content.ReadAsStringAsync());
            if (response == null)
                return new Result<LoginDto>(false, null!, "Unexpected response from the server");

            _userConfiguration.Admin = response.role == 1;
            _userConfiguration.Id = response.id;
            _userConfiguration.Identifier = response.identifier;
            _userConfiguration.Year = response.year;
        }
        catch (Exception ex)
        {
                return new Result<LoginDto>(false, null!, "Unexpected response from the server");
        }

        return new Result<LoginDto>(true, response, "");
    }

    public async Task<Result<RegisterDto>> Register(string identifier, string username, string password)
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("identifier", identifier)
        });
        HttpResponseMessage result;
        try
        {
            result = await httpClient.PostAsync("/users/register", data).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return new Result<RegisterDto>(false, null!, e.Message);
        }

        if (result.StatusCode != HttpStatusCode.Created)
            return new Result<RegisterDto>(false, null!, "User could not be created");

        if (!result.IsSuccessStatusCode)
            return new Result<RegisterDto>(false, null!, "Unknown error encountered from server");
        RegisterDto? response = null;
        try
        {
            response = JsonSerializer.Deserialize<RegisterDto>(await result.Content.ReadAsStringAsync());
            if (response == null)
                return new Result<RegisterDto>(false, null!, "Unexpected response from the server");
        }
        catch (Exception ex)
        {
            return new Result<RegisterDto>(false, null!, "Unexpected response from the server");
        }

        return new Result<RegisterDto>(true, response, "");
    }
}