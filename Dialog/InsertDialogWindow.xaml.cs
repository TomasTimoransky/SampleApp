using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.Dialog
{
    public partial class InsertDialogWindow : Window
    {
        public InsertDialogWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
