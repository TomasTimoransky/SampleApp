using Database;
using System;
using System.Linq.Expressions;

namespace WpfApp1.Common
{
    public class LambdaExpressionBuilder
    {
        private Expression<Func<CompanyEntity, bool>> expression;

        private readonly ParameterExpression x = Expression.Parameter(typeof(CompanyEntity));
        private Expression body;

        public LambdaExpressionBuilder()
        {
            GenerateInitialExpression();
        }

        //(x) => true
        private void GenerateInitialExpression()
        {
            body = Expression.Constant(true);
            expression = Expression.Lambda<Func<CompanyEntity, bool>>(body, x);
        }

        public LambdaExpressionBuilder WithId(int? id)
        {
            Expression property = Expression.Property(x, "Id");
            body = Expression.AndAlso(body, Expression.Equal(property, Expression.Constant(id)));
            expression = Expression.Lambda<Func<CompanyEntity, bool>>(body, x);
            return this;
        }

        public LambdaExpressionBuilder WithCompanyName(string companyName)
        {
            Expression property = Expression.Property(x, "Name");
            body = Expression.AndAlso(body, Expression.Equal(property, Expression.Constant(companyName)));
            expression = Expression.Lambda<Func<CompanyEntity, bool>>(body, x);
            return this;
        }

        public LambdaExpressionBuilder WithCountryCode(string countryCode)
        {
            Expression property = Expression.Property(x, "CountryCode");
            body = Expression.AndAlso(body, Expression.Equal(property, Expression.Constant(countryCode)));
            expression = Expression.Lambda<Func<CompanyEntity, bool>>(body, x);
            return this;
        }

        public LambdaExpressionBuilder WithCompanyType(int? companyType)
        {
            Expression companyTypeProperty = Expression.Property(x, "CompanyType");
            Expression companyTypePropertyId = Expression.Property(companyTypeProperty, "Id");
            body = Expression.AndAlso(body, Expression.Equal(companyTypePropertyId, Expression.Constant(companyType)));
            expression = Expression.Lambda<Func<CompanyEntity, bool>>(body, x);
            return this;
        }

        public Expression<Func<CompanyEntity, bool>> Build()
        {
            return expression;
        }
    }
}
