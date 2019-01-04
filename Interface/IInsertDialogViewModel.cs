using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp1.Model;

namespace WpfApp1.Interface
{
    //kvoli obideniu circularnej referencie
    public interface IInsertDialogViewModel
    {
        string CompanyName { get; set; }

        string CountryCode { get; set; }

        KeyValuePair<int?, string> SelectedCompanyType { get; set; }

        ObservableCollection<KeyValuePair<int?, string>> CompanyTypes { get; set; }

        bool IsDialogConfirmed { get; set; }

        ExtendedRelayCommand ConfirmCommand { get; set; }
        ExtendedRelayCommand CancelCommand { get; set; }

        Action CloseDialog { get; set; }
    }
}
