using DatabaseServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.Dialog;
using WpfApp1.Model;

namespace WpfApp1.ApplicationLogic
{
    public class PanelViewModel : ViewModelBase
    {
        public Action CloseApplicationDelegate;

        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { loading = value; RaiseNotifyPropertyChange(); RaiseNotifyPropertyChange("ResultGridEnabled"); }
        }

        public bool ResultGridEnabled
        {
            get { return !Loading; }
        }

        private string companyIdFilter;
        public string CompanyIdFilter
        {
            get { return companyIdFilter; }
            set { companyIdFilter = value; RaiseNotifyPropertyChange(); }
        }

        private string companyNameFilter;
        public string CompanyNameFilter
        {
            get { return companyNameFilter; }
            set { companyNameFilter = value; RaiseNotifyPropertyChange(); }
        }

        private string countryCodeFilter;
        public string CountryCodeFilter
        {
            get { return countryCodeFilter; }
            set { countryCodeFilter = value; RaiseNotifyPropertyChange(); }
        }

        private readonly KeyValuePair<int?, string> anyCompanyType = new KeyValuePair<int?, string>(null, "[any]");

        private KeyValuePair<int?, string> selectedCompanyTypeFilter;
        public KeyValuePair<int?, string> SelectedCompanyTypeFilter
        {
            get { return selectedCompanyTypeFilter; }
            set { selectedCompanyTypeFilter = value; RaiseNotifyPropertyChange(); }
        }

        private ObservableCollection<KeyValuePair<int?, string>> companyTypes;
        public ObservableCollection<KeyValuePair<int?, string>> CompanyTypes
        {
            get { return companyTypes; }
            set { companyTypes = value; RaiseNotifyPropertyChange(); }
        }

        public ExtendedRelayCommand FindCommand { get; set; }
        public ExtendedRelayCommand InsertCommand { get; set; }
        public ExtendedRelayCommand CloseCommand { get; set; }

        public ResultListViewModel ResultListViewModel { get; set; }

        public PanelViewModel()
        {
            ResultListViewModel = new ResultListViewModel();
            FindCommand = new ExtendedRelayCommand(OnFindCommand);
            InsertCommand = new ExtendedRelayCommand(OnInsertCommand);
            CloseCommand = new ExtendedRelayCommand(OnCloseCommand);
            InitializeData();
        }

        private void OnCloseCommand()
        {
            CloseApplicationDelegate();
        }

        private async void OnInsertCommand()
        {
            IEnumerable<KeyValuePair<int?, string>> types = await GetCompanyTypesAsync();
            InsertDialogViewModel insertDialogViewModel = new InsertDialogViewModel(types);

            DialogService dialogService = new DialogService();
            dialogService.ShowDialog(insertDialogViewModel);
            if (insertDialogViewModel.IsDialogConfirmed)
            {
                await DatabaseServiceConnector.DatabaseServiceConnector.
                    InsertCompanyAsync(insertDialogViewModel.CompanyName, 
                                       insertDialogViewModel.CountryCode, 
                                       insertDialogViewModel.SelectedCompanyType.Key);

                InitializeData();
            }
        }

        private async void OnFindCommand()
        {
            int id;
            int? companyTypeId = SelectedCompanyTypeFilter.Key;

            string companyName = CompanyNameFilter;
            if (string.IsNullOrWhiteSpace(companyName))
            {
                companyName = null;
            }

            string countryCode = CountryCodeFilter;
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                countryCode = null;
            }

            IEnumerable<CompanyDTO> companyDTOs;

            Loading = true;
            if (!int.TryParse(CompanyIdFilter, out id))
            {   
               companyDTOs = await GetCompaniesAsync(null, companyName, countryCode, companyTypeId);
            }
            else
            {
                companyDTOs = await GetCompaniesAsync(id, companyName, countryCode, companyTypeId);
                CompanyNameFilter = string.Empty;
                CountryCodeFilter = string.Empty;
                SelectedCompanyTypeFilter = anyCompanyType;
            }
            Loading = false;

            ResultListViewModel.Fill(companyDTOs);
        }

        private async void InitializeData()
        {
            Loading = true;
            CompanyCompositeDTO companyCompositeDTO = await GetAllCompaniesAsync();
            if(companyCompositeDTO != null)
            {
                FillCompanyTypesAndSelectDefault(companyCompositeDTO.CompanyTypeDTOs);
                ResultListViewModel.Fill(companyCompositeDTO.CompanyDTOs);
            }   
            Loading = false;
        }

        private void FillCompanyTypesAndSelectDefault(IEnumerable<CompanyTypeDTO> companyTypeDTOs)
        {
            List<KeyValuePair<int?, string>> keyValuePairs = ExtractCompanyTypes(companyTypeDTOs).ToList();
            keyValuePairs.Insert(0, anyCompanyType);
            CompanyTypes = new ObservableCollection<KeyValuePair<int?, string>>(keyValuePairs);
            SelectedCompanyTypeFilter = anyCompanyType;
        }

        private IEnumerable<KeyValuePair<int?, string>> ExtractCompanyTypes(IEnumerable<CompanyTypeDTO> companyTypeDTOs)
        {
            IEnumerable<KeyValuePair<int?, string>> keyValuePairs = new List<KeyValuePair<int?, string>>();
            if (companyTypeDTOs != null)
            {
                keyValuePairs = ((companyTypeDTOs).Select(x => new KeyValuePair<int?, string>(x.Id, x.Name))).ToList();
            }
            return keyValuePairs;
        }

        private async Task<IEnumerable<KeyValuePair<int?, string>>> GetCompanyTypesAsync()
        {
            CompanyCompositeDTO companyCompositeDTO = await DatabaseServiceConnector.DatabaseServiceConnector.GetAllCompaniesAsync();
            List<CompanyTypeDTO> companyTypeDTOs = new List<CompanyTypeDTO>();
            if (companyCompositeDTO != null)
            {
                companyTypeDTOs = companyCompositeDTO.CompanyTypeDTOs;
            }   
            return ExtractCompanyTypes(companyTypeDTOs);
        }

        private async Task<CompanyCompositeDTO> GetAllCompaniesAsync()
        {
            return (await DatabaseServiceConnector.DatabaseServiceConnector.GetAllCompaniesAsync());    
        }

        private async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync(int? id, string companyName, string countryCode, int? companyType)
        {
            return await DatabaseServiceConnector.DatabaseServiceConnector.GetCompaniesAsync(id, companyName, countryCode, companyType);
        }
    }
}
