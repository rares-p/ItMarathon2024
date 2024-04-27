namespace ItMarathonFrontend.Models.Subject;

public partial class Subject : ObservableObject
{
    [ObservableProperty] private bool _isEnabled = true;
    [ObservableProperty] private bool _isChecked = false;
    [ObservableProperty] private bool _viewDescription;
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public EventHandler? OnIsChecked;

    partial void OnIsCheckedChanged(bool value) => OnIsChecked?.Invoke(this, EventArgs.Empty);

    [RelayCommand]
    public void OnViewDescription()
    {
        ViewDescription = !ViewDescription;
    }
}