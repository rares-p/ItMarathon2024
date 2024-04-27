using System.Net.Http;
using Authentication.Contracts;
using Domain;
using Domain.Dto;

namespace Authentication.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserAuthenticationService(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public async Task<Result<LoginDto>> LogIn(string username, string password)
    {
        var httpClient = _httpClientFactory.CreateClient("User");
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
        });
        var result = await httpClient.PostAsync("/login", data).ConfigureAwait(false);

        return new Result<LoginDto>(true, new LoginDto(), "");
    }
}