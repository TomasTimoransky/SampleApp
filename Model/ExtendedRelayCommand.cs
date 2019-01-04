using System;
using System.Windows.Input;

namespace WpfApp1.Model
{
    public class ExtendedRelayCommand : ICommand
    {
        private Action action;

        public ExtendedRelayCommand(Action _action)
        {
            this.action = _action;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
            }

            remove
            {
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            action();
        }
    }
}
