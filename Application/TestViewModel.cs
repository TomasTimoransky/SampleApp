using Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ApplicationLogic
{
    public class TestViewModel : ViewModelBase
    {
        private string bindedText;

        public string BindedText
        {
            get { return bindedText; }
            set { bindedText = value; RaiseNotifyPropertyChange(); }
        }

        public ExtendedRelayCommand Command { get; set; }

        public TestViewModel()
        {
            Command = new ExtendedRelayCommand(OnCommand);
        }

        private void OnCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            LoadData(openFileDialog.FileName);
        }

        private async void LoadData(string path)
        {
            Helper helper = new Helper();
            Task<byte[]> task = new Task<byte[]>(() => { return helper.ReadFile(path); });
            task.Start();
            byte[] array = await task;
            BindedText = string.Join("", array);
        }
    }
}
