using ItMarathonFrontend.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace ItMarathonFrontend.Views.Pages
{
    /// <summary>
    /// Interaction logic for SubjectPreferences.xaml
    /// </summary>
    public partial class SubjectPreferencesPage : INavigableView<SubjectPreferencesViewModel>
    {
        public SubjectPreferencesViewModel ViewModel { get; }

        public SubjectPreferencesPage(SubjectPreferencesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

    }
}
