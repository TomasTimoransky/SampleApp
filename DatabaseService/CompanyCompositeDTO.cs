using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DatabaseService
{
    [DataContract]
    public class CompanyCompositeDTO
    {
        [DataMember]
        public IEnumerable<CompanyDTO> CompanyDTOs { get; set; }
        [DataMember]
        public IEnumerable<CompanyTypeDTO> CompanyTypeDTOs { get; set; }

        public CompanyCompositeDTO()
        {
        }
    }
}