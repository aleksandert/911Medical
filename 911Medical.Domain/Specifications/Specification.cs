using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _911Medical.Domain.Specifications
{
    public class BaseSpecification<T> : Specification<T> where T:class
    {
        private Action<ISpecificationBuilder<T>> build;

        public Action<ISpecificationBuilder<T>> Build { get; set; }

        public BaseSpecification()
        { 
        
        }
        public BaseSpecification(Action<ISpecificationBuilder<T>> build)
        {
            build(Query);
        }

        public IIncludableSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            return Query.Include(includeExpression);
        }


        public static BaseSpecification<T> Create(Action<ISpecificationBuilder<T>> build)
        {
            return new BaseSpecification<T>(build);
        }

       

    }
}
