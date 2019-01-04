using DatabaseServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp1.DatabaseServiceConnector
{
    public static class DatabaseServiceConnector
    {
        public static async Task<CompanyCompositeDTO> GetAllCompaniesAsync()
        {
            DatabaseServiceClient serviceClient = new DatabaseServiceClient();
            return await serviceClient.GetAllCompaniesAsync();
        }

        public static async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync(int? Id, string companyName, string countryCode, int? companyType)
        {
            DatabaseServiceClient serviceClient = new DatabaseServiceClient();
            return await serviceClient.GetCompaniesAsync(Id, companyName, countryCode, companyType);
        }

        public static async Task<bool> InsertCompanyAsync(string companyName, string countryCode, int? companyType)
        {
            DatabaseServiceClient serviceClient = new DatabaseServiceClient();
            return await serviceClient.InsertCompanyAsync(companyName, countryCode, companyType);
        }
    }
}
