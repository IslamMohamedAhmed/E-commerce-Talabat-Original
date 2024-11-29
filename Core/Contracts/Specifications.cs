using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public abstract class Specifications<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public List<Expression<Func<T, object>>> IncludesExpression = new();

        protected void AppIncludes(Expression<Func<T, object>> expression)
        {

            IncludesExpression.Add(expression);
        }  protected void SetOrder(Expression<Func<T, object>> expression)
        {

            OrderBy = expression;
        }  protected void SetOrderDescending(Expression<Func<T, object>> expression)
        {

            OrderByDescending = expression;
        } 

        protected Specifications(Expression<Func<T,bool>> criteria)
        {
            this.Criteria = criteria;
        }



    }
}
