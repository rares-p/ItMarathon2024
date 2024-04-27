using ItMarathonFrontend.Models.Subject;
using SubjectAdministration.Contracts;
using Wpf.Ui.Controls;

namespace ItMarathonFrontend.ViewModels.Pages;
public partial class SubjectPreferencesViewModel : ObservableObject, INavigationAware
{
    private readonly ISubjectAdministrationService _subjectAdministrationService;
    private readonly ISnackbarService _snackbarService;
    private bool _isInitialized;
    [field: ObservableProperty] private List<Subject>? _subjects = null;

    public SubjectPreferencesViewModel(ISubjectAdministrationService subjectAdministrationService, ISnackbarService snackbarService)
    {
        _subjectAdministrationService = subjectAdministrationService;
        _snackbarService = snackbarService;
    }

    public async void OnNavigatedTo()
    {
        if (!_isInitialized || Subjects is null)
            await InitializeViewModel();
    }

    private async Task InitializeViewModel()
    {
        var subjectsResponse = await _subjectAdministrationService.GetAllSubjects();

        if (!subjectsResponse.Success)
        {
            _snackbarService.Show("Error!", subjectsResponse.Error);
            return;
        }

        Subjects = new List<Subject>(subjectsResponse.Value.Select(subjectDto => new Subject()
        {
            Id = subjectDto.Id,
            Name = subjectDto.Name,
            Description = subjectDto.Description,
            Package = subjectDto.Package
        }));

        _isInitialized = true;
    }

    public void OnNavigatedFrom()
    {
    }
}