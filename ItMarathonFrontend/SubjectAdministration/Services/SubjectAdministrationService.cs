using System.Net.Http.Headers;
using System.Net.Http.Json;
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
}