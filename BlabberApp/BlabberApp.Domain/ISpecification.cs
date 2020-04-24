using System;
using System.Linq.Expressions;

namespace BlabberApp.Domain
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}