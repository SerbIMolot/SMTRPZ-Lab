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
using System.Windows.Shapes;

namespace SMTRPZ_IT_company
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        string showText { get; set; }
        public bool confirm = false;
        public ConfirmationWindow(string text)
        {
            InitializeComponent();
            showText = text;
            TextToRead.Text = showText;
        }

        private void OkBtn(object sender, RoutedEventArgs e)
        {
            confirm = true;
            Close();
        }

        private void CanselBtn(object sender, RoutedEventArgs e)
        {
            confirm = false;
            Close();
        }
    }
}
