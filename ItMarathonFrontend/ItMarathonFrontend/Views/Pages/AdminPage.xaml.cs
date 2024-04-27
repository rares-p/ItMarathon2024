using ItMarathonFrontend.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace ItMarathonFrontend.Views.Pages;

public partial class AdminPage : INavigableView<AdminViewModel>
{
    public AdminViewModel ViewModel { get; }

    public AdminPage(AdminViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    private void CreateUserFlyoutShow(object sender, RoutedEventArgs e)
    {
        CreateUserFlyout.Show();
    }
}