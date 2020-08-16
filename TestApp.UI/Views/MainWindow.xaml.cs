using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TestApp.UI.Controllers;
using TestApp.UI.ViewModels;

namespace TestApp.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ICompanyController companyController)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(companyController);
        }
    }
}
