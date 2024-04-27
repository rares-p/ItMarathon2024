using Domain;
using Domain.Dto;
using ItMarathonFrontend.Models;
using Services.Contracts;
using Wpf.Ui.Controls;

using EntityUser = Domain.Entities.User;

namespace ItMarathonFrontend.ViewModels.Pages;

public partial class AdminViewModel : ObservableObject, INavigationAware
{
    private readonly IAdminService _adminService;
    private readonly ISnackbarService _snackbarService;
    private readonly UserConfiguration _userConfiguration;
    private bool _isInitialized;
    [ObservableProperty]
    private bool _isCreatedUserAdmin;
    [ObservableProperty]
    private EntityUser _createdUser = new EntityUser();
    [ObservableProperty]
    private List<User>? _users = null;

    public AdminViewModel(IAdminService adminService, ISnackbarService snackbarService, UserConfiguration userConfiguration)
    {
        _adminService = adminService;
        _snackbarService = snackbarService;
        _userConfiguration = userConfiguration;
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
        if (!_userConfiguration.Admin)
        {
            _snackbarService.Show("Error!", $"Only admins can create users");
            return;
        }
        Result<CreateUserDto> result = IsCreatedUserAdmin ? await _adminService.CreateAdminUserAsync() : await _adminService.CreateUserAsync(CreatedUser);
        if(result.Success)
            CreatedUser.Identifier = result.Value.identifier;
        else
            _snackbarService.Show("Couldn't create user!", result.Error);
    }
}