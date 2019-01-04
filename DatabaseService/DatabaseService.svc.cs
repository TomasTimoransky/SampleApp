using NHibernate;
using System.Collections.Generic;
using System.Linq;
using Database;
using System;
using WpfApp1.Common;

namespace DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        public CompanyCompositeDTO GetAllCompanies()
        {
            try
            {
                List<CompanyDTO> companies = new List<CompanyDTO>();
                List<CompanyTypeDTO> companyTypes = new List<CompanyTypeDTO>();
                CompanyCompositeDTO companyCompositeDTO = new CompanyCompositeDTO();
                using (ISession session = DatabaseHelper.OpenSession())
                {
                    companies = session.Query<CompanyEntity>().Select(x => new CompanyDTO(x)).ToList();
                    companyTypes = session.Query<CompanyTypeEntity>().Select(x => new CompanyTypeDTO(x)).ToList();
                    companyCompositeDTO.CompanyDTOs = companies;
                    companyCompositeDTO.CompanyTypeDTOs = companyTypes;
                }
                return companyCompositeDTO;
            }
            catch
            {
                return null;
            }
            
        }

        public IEnumerable<CompanyDTO> GetCompanies(int? Id, string companyName, string countryCode, int? companyType)
        {
            try
            {
                LambdaExpressionBuilder expressionBuilder = new LambdaExpressionBuilder();

                List<CompanyDTO> companies = new List<CompanyDTO>();
                using (ISession session = DatabaseHelper.OpenSession())
                {
                    if (Id != null)
                    {
                        expressionBuilder = expressionBuilder.WithId(Id);

                        companies = session.Query<CompanyEntity>()
                            .Where(expressionBuilder.Build()).Select(x => new CompanyDTO(x))
                            .ToList();
                    }
                    else
                    {
                        if (companyType != null)
                        {
                            expressionBuilder = expressionBuilder.WithCompanyType(companyType);
                        }
                        if (companyName != null)
                        {
                            expressionBuilder = expressionBuilder.WithCompanyName(companyName);
                        }
                        if (countryCode != null)
                        {
                            expressionBuilder = expressionBuilder.WithCountryCode(countryCode);
                        }
                        companies = session.Query<CompanyEntity>()
                            .Where(expressionBuilder.Build()).Select(x => new CompanyDTO(x))
                            .ToList();
                    }
                }
                return companies;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public bool InsertCompany(string companyName, string coutryCode, int? companyType)
        {
            try
            {
                using (ISession session = DatabaseHelper.OpenSession())
                {
                    if (companyType == null)
                    {
                        session.Save(new CompanyEntity() { Name = companyName, CountryCode = coutryCode, CompanyType = null });
                    }
                    else
                    {
                        CompanyTypeEntity companyTypeEntity = session.Query<CompanyTypeEntity>().Where(x => x.Id == companyType).SingleOrDefault();
                        session.Save(new CompanyEntity() { Name = companyName, CountryCode = coutryCode, CompanyType = companyTypeEntity });
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
