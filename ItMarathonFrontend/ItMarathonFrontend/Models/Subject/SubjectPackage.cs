using System.Windows.Documents;

namespace ItMarathonFrontend.Models.Subject;

public partial class SubjectPackage : ObservableObject
{
    public uint Name { get; set; }
    [ObservableProperty]
    private List<Subject> _subjects = new();
}