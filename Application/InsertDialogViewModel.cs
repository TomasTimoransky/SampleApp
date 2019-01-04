using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Interface;
using WpfApp1.Model;

namespace WpfApp1.ApplicationLogic
{
    public class InsertDialogViewModel : ViewModelBase, IInsertDialogViewModel
    {
        private string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; RaiseNotifyPropertyChange(); }
        }

        private string countryCode;
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; RaiseNotifyPropertyChange(); }
        }

        private KeyValuePair<int?, string> selectedCompanyType;
        public KeyValuePair<int?, string> SelectedCompanyType
        {
            get { return selectedCompanyType; }
            set { selectedCompanyType = value; RaiseNotifyPropertyChange(); }
        }

        private ObservableCollection<KeyValuePair<int?, string>> companyTypes;
        public ObservableCollection<KeyValuePair<int?, string>> CompanyTypes
        {
            get { return companyTypes; }
            set { companyTypes = value; RaiseNotifyPropertyChange(); }
        }

        public bool IsDialogConfirmed { get; set; }

        public ExtendedRelayCommand ConfirmCommand { get; set; }
        public ExtendedRelayCommand CancelCommand { get; set; }
        public Action CloseDialog { get; set; }

        public InsertDialogViewModel(IEnumerable<KeyValuePair<int?, string>> types)
        {
            List<KeyValuePair<int?, string>> keyValuePairs = new List<KeyValuePair<int?, string>>(types);
            keyValuePairs.Insert(0, new KeyValuePair<int?, string>(null, "n/a"));
            CompanyTypes = new ObservableCollection<KeyValuePair<int?, string>>(keyValuePairs);
            SelectedCompanyType = keyValuePairs.FirstOrDefault();

            ConfirmCommand = new ExtendedRelayCommand(OnConfirmCommand);
            CancelCommand = new ExtendedRelayCommand(OnCancelCommand);
            IsDialogConfirmed = false;
        }

        private void OnCancelCommand()
        {
            CloseDialog();
        }

        private void OnConfirmCommand()
        {
            IsDialogConfirmed = true;
            CloseDialog();
        }  
    }
}
