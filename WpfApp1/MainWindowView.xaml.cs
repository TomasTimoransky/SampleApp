using System.Windows;
using System.Windows.Input;

namespace WpfApp1.MainWindow
{
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            ((MainWindowViewModel)this.DataContext).CloseApplicationDelegate = this.Close;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {           
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();    
        }
    }
}
