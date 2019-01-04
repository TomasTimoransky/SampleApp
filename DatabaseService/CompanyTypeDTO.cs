using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;

namespace DatabaseService
{
    [DataContract]
    public class CompanyTypeDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }

        public CompanyTypeDTO(int id, string name, string description, List<CompanyDTO> companies)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public CompanyTypeDTO(CompanyTypeEntity companyTypeEntity)
        {
            Id = companyTypeEntity.Id;
            Name = companyTypeEntity.Name;
            Description = companyTypeEntity.Description;
        }
    }
}