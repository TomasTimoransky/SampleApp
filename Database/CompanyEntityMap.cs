using FluentNHibernate.Mapping;

namespace Database
{
    class CompanyEntityMap : ClassMap<CompanyEntity>
    {
        public CompanyEntityMap()
        {
            Schema("dbo");
            Table("COMPANY");
            Id(x => x.Id).Column("ID").Not.Nullable();
            Map(x => x.Name).Column("NAME");
            Map(x => x.CountryCode).Column("COUNTRY_CODE");
            References(x => x.CompanyType, "FK_COMPANY_TYPE").Cascade.None();
        }
    }
}
