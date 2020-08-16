using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestApp.UI.ViewModels;

namespace TestApp.UI.Infrastructure.Dialogs
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string errorMessage = null, string additionalInfo = null)
        {
            InitializeComponent();
            DataContext = new ErrorWindowViewModel()
            {
                ErrorMessage = errorMessage,
                AdditionalErrorInfo = additionalInfo
            };
        }
    }

    public class ErrorWindowViewModel : BaseViewModel
    {
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        private string _additionalErrorInfo;
        public string AdditionalErrorInfo
        {
            get => _additionalErrorInfo;
            set
            {
                _additionalErrorInfo = value;
                OnPropertyChanged();
            }
        }
    }
}
