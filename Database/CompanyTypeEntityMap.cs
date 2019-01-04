using FluentNHibernate.Mapping;

namespace Database
{
    class CompanyTypeEntityMap : ClassMap<CompanyTypeEntity>
    {
        public CompanyTypeEntityMap()
        {
            Schema("dbo");
            Table("COMPANY_TYPE");
            Id(x => x.Id).Column("ID").Not.Nullable();
            Map(x => x.Name).Column("NAME");
            Map(x => x.Description).Column("DESCRIPTION");
        }
    }
}
