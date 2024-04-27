using ItMarathonFrontend.Models;
using Services.Contracts;
using Wpf.Ui.Controls;

using EntityUser = Domain.Entities.User;

namespace ItMarathonFrontend.ViewModels.Pages;

public partial class AdminViewModel : ObservableObject, INavigationAware
{
    private readonly IAdminService _adminService;
    private bool _isInitialized;
    [ObservableProperty]
    private bool _isCreatedUserAdmin;
    [ObservableProperty]
    private EntityUser _createdUser = new EntityUser();
    [ObservableProperty]
    private List<User>? _users = null;

    public AdminViewModel(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async void OnNavigatedTo()
    {
        if (!_isInitialized || Users is null)
            await InitializeViewModel();
        _isInitialized = true;
    }

    private async Task InitializeViewModel()
    {
        Users = new();

        for (int i = 0; i < 10; i++)
        {
            Users.Add(new User()
            {
                Identifier = $"Student{i}",
                Credits = (uint)(i * 7),
                Grade = (uint)(i * 7 / 2.6),
                Name = "MyName",
                Year = (uint)i
            });
        }
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    public async Task OnCreateUser()
    {
        if (IsCreatedUserAdmin)
            _adminService.CreateUserAsync(CreatedUser.Identifier);
        else
            _adminService.CreateUserAsync(CreatedUser);
    }
}