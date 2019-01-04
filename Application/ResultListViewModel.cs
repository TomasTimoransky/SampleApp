using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DatabaseServiceReference;
using WpfApp1.Model;

namespace WpfApp1.ApplicationLogic
{
    public class ResultListViewModel : ViewModelBase
    {
        private ObservableCollection<ResultItem> resultItemsList;

        public ObservableCollection<ResultItem> ResultItemsList
        {
            get { return resultItemsList; }
            set { resultItemsList = value; RaiseNotifyPropertyChange(); }
        }

        public ResultListViewModel()
        {
            ResultItemsList = new ObservableCollection<ResultItem>();      
        }

        public void Fill(IEnumerable<CompanyDTO> companyDTOs)
        {
            if (companyDTOs != null)
            {
                ResultItemsList = new ObservableCollection<ResultItem>((companyDTOs).Select(x => new ResultItem()
                {
                    CompanyId = x.Id,
                    CompanyName = x.Name,
                    CompanyCountryCode = x.CountryCode,
                    CompanyType = x.CompanyType != null ? x.CompanyType.Name : "[n/a]"
                }));
            }
        }
    }
}
