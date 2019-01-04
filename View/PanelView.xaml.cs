using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for PanelView.xaml
    /// </summary>
    public partial class PanelView : UserControl
    {
        public PanelView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
