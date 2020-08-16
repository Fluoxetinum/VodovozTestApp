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
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public QuestionWindow(string question)
        {
            InitializeComponent();
            DataContext = new QuestionWindowViewModel()
            {
                Question = question
            };
        }

        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }

    public class QuestionWindowViewModel : BaseViewModel
    {
        private string _question;
        public string Question
        {
            get => _question;
            set
            {
                _question = value;
                OnPropertyChanged();
            }
        }
    }

}
