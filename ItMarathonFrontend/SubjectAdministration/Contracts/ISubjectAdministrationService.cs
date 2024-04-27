using Domain;
using Domain.Dto;

namespace SubjectAdministration.Contracts;

public interface ISubjectAdministrationService
{
    Task<Result<AllSubjectsDto>> GetAllSubjects();
    Task<Result<HttpResponseMessage>> SendPreferencesAsync(List<string> preferences);
}