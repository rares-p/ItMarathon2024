using System.Windows.Documents;

namespace ItMarathonFrontend.Models.Subject;

public partial class SubjectPackage : ObservableObject
{
    public string Name { get; set; }
    [ObservableProperty]
    private List<Subject> _subjects = new();
}