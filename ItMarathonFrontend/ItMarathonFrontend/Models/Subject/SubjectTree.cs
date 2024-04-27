namespace ItMarathonFrontend.Models.Subject;

public partial class SubjectTree : ObservableObject
{
    [ObservableProperty] private List<SubjectPackage> _subjectPackages = new();
}