﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Authentication.Contracts;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace ItMarathonFrontend.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly ISnackbarService _snackbarService;
        [ObservableProperty]
        private string _identifier;
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        public MainWindowViewModel(IUserAuthenticationService userAuthenticationService, ISnackbarService snackbarService)
        {
            _userAuthenticationService = userAuthenticationService;
            _snackbarService = snackbarService;
        }

        [ObservableProperty] private bool _isUserLoggedIn = false;

        [ObservableProperty]
        private string _applicationTitle = "WPF UI - ItMarathonFrontend";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Data",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "Subject Preferences",
                Icon = new SymbolIcon {Symbol = SymbolRegular.TableSimpleCheckmark24},
                TargetPageType = typeof(Views.Pages.SubjectPreferencesPage)
            },
            new NavigationViewItem()
            {
                Content = "Admin Dashboard",
                Icon = new SymbolIcon {Symbol = SymbolRegular.DataUsageEdit24},
                TargetPageType = typeof(Views.Pages.AdminPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        [RelayCommand]
        private async Task OnUserSignIn()
        {
            //IsUserLoggedIn = true;
            //return;
            var loginResponse = await _userAuthenticationService.LogIn(Username, Password);
            if (!loginResponse.Success)
            {
                _snackbarService.Show("Error!", loginResponse.Error);
                MessageBox error = new MessageBox()
                {
                    Title = "Error!",
                    Content = loginResponse.Error
                };
                await error.ShowDialogAsync();
            }
            else
                IsUserLoggedIn = true;
        }

        [RelayCommand]
        private async Task OnUserRegister()
        {
            var registerResponse = await _userAuthenticationService.Register(Identifier, Username, Password);
            if (!registerResponse.Success)
            {
                _snackbarService.Show("Error!", registerResponse.Error);
                MessageBox error = new MessageBox()
                {
                    Title = "Error!",
                    Content = registerResponse.Error
                };
                await error.ShowDialogAsync();
            }
            else
            {
                MessageBox done = new MessageBox()
                {
                    Title = "Done!",
                    Content = $"User with identifier {Identifier} registered successfully!"
            };
                await done.ShowDialogAsync();
            }
        }
    }
}
