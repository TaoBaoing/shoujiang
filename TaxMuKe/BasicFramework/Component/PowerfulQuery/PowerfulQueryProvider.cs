using System;
using System.Linq;
using System.Linq.Expressions;

namespace BasicFramework.Component
{
    /// <summary>
    /// 包装IQueryProvider，以拦截对表达式目录树的访问
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class PowerfulQueryProvider<T> : IQueryProvider
    {
        private readonly PowerfulQuery<T> _query;

        internal PowerfulQueryProvider(PowerfulQuery<T> query)
        {
            this._query = query;
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            return this._query.InnerQuery.Provider.CreateQuery(expression.Power());
        }

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            return new PowerfulQuery<TElement>(this._query.InnerQuery.Provider.CreateQuery<TElement>(expression.Power()));
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return this._query.InnerQuery.Provider.Execute(expression.Power());
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            return this._query.InnerQuery.Provider.Execute<TResult>(expression.Power());
        }
    }
}


