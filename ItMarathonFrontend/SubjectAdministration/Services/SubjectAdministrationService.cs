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

    public async Task<Result<AllSubjectsDto>> GetAllSubjects()
    {
        var httpClient = _httpClientFactory.CreateClient("Default");
        var getAllSubjectsResponse = await httpClient.GetAsync("/subjects");

        if (getAllSubjectsResponse is null || !getAllSubjectsResponse.IsSuccessStatusCode)
            return new Result<AllSubjectsDto>(false, null!, "Am intampinat o eroare");
        AllSubjectsDto? subjectsDto = null;
        var test = await getAllSubjectsResponse.Content.ReadAsStringAsync();
        try
        {
            subjectsDto = JsonSerializer.Deserialize<AllSubjectsDto>(await getAllSubjectsResponse.Content.ReadAsStringAsync());
        }
        catch (Exception ex)
        {
            return new Result<AllSubjectsDto>(false, null!, "Am intampinat o eroare");
        }

        if (subjectsDto is null)
            return new Result<AllSubjectsDto>(false, null!, "Am intampinat o eroare");

        return new Result<AllSubjectsDto>(true, subjectsDto, "");
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

        if (!result.IsSuccessStatusCode || result.StatusCode != HttpStatusCode.OK)
            return new Result<HttpResponseMessage>(false, null!, "Unknown error encountered from server");

        return new Result<HttpResponseMessage>(true, null, "");
    }
}