using System.Windows.Documents;

namespace ItMarathonFrontend.Models.Subject;

public partial class SubjectPackage : ObservableObject
{
    public string Name { get; set; }
    [ObservableProperty]
    private List<Subject> _subjects = new List<Subject>();

    partial void OnSubjectsChanged(List<Subject> value)
    {
        foreach (var subject in value)
        {
            if (subject.OnIsChecked == null!)
                subject.OnIsChecked += OnSubjectIsCheckedChanged!;
        }
    }

    public void OnSubjectIsCheckedChanged(object sender, EventArgs e)
    {
        if (sender is not Subject subject) return;
        foreach (var sub in Subjects.Where(sub => sub != subject))
            sub.IsEnabled = !subject.IsChecked;
    }
}