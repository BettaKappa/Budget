using System.ComponentModel;
using System.Windows;

namespace XMusic
{
    public partial class InputDialog : Window
    {
        public string Answer { get; set; }

        public InputDialog()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                Answer = inTag.Text;
                DialogResult = true;
        }

        private void InputDialog_OnClosing(object? sender, CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                DialogResult = false;
            }
        }
    }
}
