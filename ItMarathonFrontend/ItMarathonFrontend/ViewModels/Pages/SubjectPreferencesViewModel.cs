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
    [ObservableProperty] private List<SubjectPackage> _subjectPackages = new();

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
        for (int i = 0; i < 1; i++)
        {
            SubjectPackages.Add(new SubjectPackage()
            {
                Name = $"Package {i}",
                Subjects = new List<Subject>()
                {
                    new Subject()
                    {
                        Description = "asd",
                        Name = "test"
                    },
                    new Subject()
                    {
                        Description = "asd",
                        Name = "test"
                    }
                }
            });
        }
        //var subjectsResponse = await _subjectAdministrationService.GetAllSubjects();

        //if (!subjectsResponse.Success)
        //{
        //    _snackbarService.Show("Error!", subjectsResponse.Error);
        //    return;
        //}

        //Subjects = new List<Subject>(subjectsResponse.Value.Select(subjectDto => new Subject()
        //{
        //    Id = subjectDto.Id,
        //    Name = subjectDto.Name,
        //    Description = subjectDto.Description,
        //    Package = subjectDto.Package
        //}));

        _isInitialized = true;
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    public async Task OnSendPreferences()
    {
        foreach (var subject in SubjectPackages)
        {
            if (subject.Subjects.Any(x => x.Priority == 0))
            {
                _snackbarService.Show("Error!", $"Cannot have priority be equal to 0 for package {subject.Name}");
                return;
            }
            var values = subject.Subjects.Select(x => x.Priority).Distinct().Count();
            if (values != subject.Subjects.Count())
            {
                _snackbarService.Show("Error!", $"Cannot have duplicate priorities for package {subject.Name}");
                return;
            }
        }

        foreach (var package in SubjectPackages)
        {
            var currentPackagePreferences = new List<string>();
            for(int i = 0; i < package.Subjects.Count; i++) currentPackagePreferences.AddRange(from sub in package.Subjects where sub.Priority == i + 1 select sub.Id);

            await _subjectAdministrationService.SendPreferencesAsync(currentPackagePreferences);
        }
    }
}