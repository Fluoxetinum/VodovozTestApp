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

namespace TestApp.UI.Views.Entities
{
    /// <summary>
    /// Interaction logic for DivisionWindow.xaml
    /// </summary>
    public partial class DivisionWindow : Window
    {
        public DivisionWindow()
        {
            InitializeComponent();
        }

        private void ActionButtonOnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
