using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T>
    {
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecifications()
        {

        }


        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> includes { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude (Expression<Func<T, object>> IncludeExpression)
        {
            includes.Add(IncludeExpression);
        }

        protected void AddOrderBy (Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

         protected void AddOrderByDescending (Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

           protected void ApplyPaging (int skip, int take)
        {
            Skip=skip;
            Take=take;
            IsPagingEnabled=true;

        }
    }
}