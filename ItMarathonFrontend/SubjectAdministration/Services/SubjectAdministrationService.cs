using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.Dto;
using SubjectAdministration.Contracts;

namespace SubjectAdministration.Services;

public class SubjectAdministrationService : ISubjectAdministrationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserConfiguration _userConfiguration;

    public SubjectAdministrationService(IHttpClientFactory httpClientFactory, UserConfiguration userConfiguration)
    {
        _httpClientFactory = httpClientFactory;
        _userConfiguration = userConfiguration;
    }

    public async Task<Result<List<SubjectDto>>> GetAllSubjects()
    {
        //var httpClient = _httpClientFactory.CreateClient("Default");
        //httpClient.DefaultRequestHeaders.Authorization =
        //    new AuthenticationHeaderValue("Bearer", _userConfiguration.Token);
        //var getAllSubjectsResponse = await httpClient.GetAsync("/subjects");

        //if (getAllSubjectsResponse is null || !getAllSubjectsResponse.IsSuccessStatusCode)
        //    return new Result<List<SubjectDto>>(false, null!, "Am intampinat o eroare");
        //List<SubjectDto>? subjectsDto = null;
        //try
        //{
        //    subjectsDto = await getAllSubjectsResponse.Content.ReadFromJsonAsync<List<SubjectDto>>();
        //}
        //catch (Exception ex)
        //{
        //    return new Result<List<SubjectDto>>(false, null!, "Am intampinat o eroare");
        //}

        //if(subjectsDto is null)
        //    return new Result<List<SubjectDto>>(false, null!, "Am intampinat o eroare");

        //return new Result<List<SubjectDto>>(true, subjectsDto, "");

        return new Result<List<SubjectDto>>(true, new List<SubjectDto>(), "");
    }

    public async Task<Result<HttpResponseMessage>> SendPreferencesAsync(List<string> preferences)
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("studentId", _userConfiguration.Id),
            new KeyValuePair<string, string>("preferences", preferences.ToString()!)
        });
        HttpResponseMessage result;
        try
        {
            result = await httpClient.PutAsync("/preferences/", data).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return new Result<HttpResponseMessage>(false, null!, e.Message);
        }

        if (!result.IsSuccessStatusCode)
            return new Result<HttpResponseMessage>(false, null!, "Unknown error encountered from server");
        File.WriteAllText("C:\\Users\\panai\\Desktop\\test.txt", data.ToString());
        //File.WriteAllText("C:\\Users\\panai\\Desktop\\test.txt", await result.Content.ReadAsStringAsync());
        //LoginDto? response = null;
        //try
        //{
        //    response = JsonSerializer.Deserialize<LoginDto>(await result.Content.ReadAsStringAsync());
        //    if (response == null)
        //        return new Result<LoginDto>(false, null!, "Unexpected response from the server");

        //    _userConfiguration.Admin = response.role == 1;
        //    _userConfiguration.Id = response.id;
        //    _userConfiguration.Identifier = response.identifier;
        //}
        //catch (Exception ex)
        //{
        //    return new Result<LoginDto>(false, null!, "Unexpected response from the server");
        //}

        return new Result<HttpResponseMessage>(true, null, "");
    }
}