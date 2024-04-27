namespace ItMarathonFrontend.Models.Subject;

public partial class Subject : ObservableObject
{
    [ObservableProperty] private List<int> _priorities = new() { 1, 2, 3, 4 };
    [ObservableProperty] private int _priority = 0;
    [ObservableProperty] private bool _isChecked = false;
    [ObservableProperty] private bool _viewDescription;
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    [RelayCommand]
    public void OnViewDescription()
    {
        ViewDescription = !ViewDescription;
    }
}