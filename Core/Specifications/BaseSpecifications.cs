using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T>
    {
        public BaseSpecifications(Expression<Func<T,bool>>criteria)
        {
            Criteria = criteria ;
        }
          public BaseSpecifications()
        {
          
        }


        public Expression<Func<T, bool>> Criteria   {get;}

        public List<Expression<Func<T, object>>> includes {get;} = new List<Expression<Func<T, object>>>();
        
        protected void AddInclude(Expression<Func<T,object>> IncludeExpression)
        {
            includes.Add(IncludeExpression);
        }
    }
}