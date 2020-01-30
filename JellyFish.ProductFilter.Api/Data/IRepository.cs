using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProductFilter.Api.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll { get; }

        IQueryable<T> Get(Expression<Func<T, bool>> queryExpression);
    }
}