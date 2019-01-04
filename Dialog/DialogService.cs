using WpfApp1.Interface;

namespace WpfApp1.Dialog
{
    public class DialogService
    {
        public bool? ShowDialog(IInsertDialogViewModel datacontext)
        {
            var window = new InsertDialogWindow();
            window.WindowStyle = System.Windows.WindowStyle.None;
            window.ResizeMode = System.Windows.ResizeMode.NoResize;
            datacontext.CloseDialog = window.Close;
            window.DataContext = datacontext;

            return window.ShowDialog();
        }
    }
}
