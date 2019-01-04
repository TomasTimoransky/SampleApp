using System.Collections.Generic;
using System.ServiceModel;

namespace DatabaseService
{
    [ServiceContract]
    public interface IDatabaseService
    {
        [OperationContract]
        CompanyCompositeDTO GetAllCompanies();

        [OperationContract]
        IEnumerable<CompanyDTO> GetCompanies(int? Id, string companyName, string countryCode, int? companyType);

        [OperationContract]
        bool InsertCompany(string companyName, string countryCode, int? companyType);
    }
}
