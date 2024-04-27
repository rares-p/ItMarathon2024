using Domain;
using Domain.Dto;

namespace SubjectAdministration.Contracts;

public interface ISubjectAdministrationService
{
    Task<Result<List<SubjectDto>>> GetAllSubjects();
}