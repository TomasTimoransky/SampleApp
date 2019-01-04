using System.Runtime.Serialization;
using Database;

namespace DatabaseService
{
    [DataContract]
    public class CompanyDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public CompanyTypeDTO CompanyType { get; set; }

        public CompanyDTO(int id, string name, string countryCode, CompanyTypeDTO companyType)
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            CompanyType = companyType;
        }

        public CompanyDTO(CompanyEntity companyEntity)
        {
            Id = companyEntity.Id;
            Name = companyEntity.Name;
            CountryCode = companyEntity.CountryCode;
            if(companyEntity.CompanyType != null)
            {
                CompanyType = new CompanyTypeDTO(companyEntity.CompanyType);
            }
        }
    }
}