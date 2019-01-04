namespace Database
{
    public class CompanyEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual CompanyTypeEntity CompanyType { get; set; }
    }
}
