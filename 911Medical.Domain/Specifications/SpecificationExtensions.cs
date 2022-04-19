using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public static class SpecificationExtensions
    {
        public static S Combine<T, S>(this S first, ISpecification<T> other)
            where T : class
            where S : Specification<T>
        {
            var specificationBuilder = new SpecificationBuilder<T>(first);

            return specificationBuilder.Combine(other).Specification as S;
        }

        public static ISpecificationBuilder<T> Combine<T>(this ISpecificationBuilder<T> specificationBuilder, ISpecification<T> other) where T : class
        {
            foreach (var includeExpression in other.IncludeExpressions)
            {
                ((List<IncludeExpressionInfo>)specificationBuilder.Specification.IncludeExpressions).Add(includeExpression);
            };

            foreach (var includeString in other.IncludeStrings)
            {
                ((List<string>)specificationBuilder.Specification.IncludeStrings).Add(includeString);
            };

            foreach (var orderExpression in other.OrderExpressions)
            {
                ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions).Add(orderExpression);
            };

            foreach (var whereExpression in other.WhereExpressions)
            {
                ((List<WhereExpressionInfo<T>>)specificationBuilder.Specification.WhereExpressions).Add(whereExpression);
            };

            foreach (var searchCriteria in other.SearchCriterias)
            {
                ((List<SearchExpressionInfo<T>>)specificationBuilder.Specification.SearchCriterias).Add(searchCriteria);
            };

            return specificationBuilder;
        }

        
    }
}
