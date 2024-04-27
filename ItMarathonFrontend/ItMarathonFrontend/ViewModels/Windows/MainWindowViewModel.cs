// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Authentication.Contracts;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace ItMarathonFrontend.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private IUserAuthenticationService _userAuthenticationService;
        public MainWindowViewModel(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
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
        private void OnUserSignIn()
        {
            IsUserLoggedIn = true;
            _userAuthenticationService.LogIn("asd", "asd");
        }
    }
}
