using WpfApp1.ApplicationLogic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace WpfApp1.MainWindow
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Action CloseApplicationDelegate;

        private PanelViewModel panelViewModel;

        public PanelViewModel PanelViewModel
        {
            get { return panelViewModel; }
            set { panelViewModel = value; RaiseNotifyPropertyChange(); }
        }

        private TestViewModel testViewModel;

        public TestViewModel TestViewModel
        {
            get { return testViewModel; }
            set { testViewModel = value; RaiseNotifyPropertyChange(); }
        }

        public MainWindowViewModel()
        {
            panelViewModel = new PanelViewModel();
            testViewModel = new TestViewModel();
            panelViewModel.CloseApplicationDelegate = () => CloseApplicationDelegate();
        }
    }
}
