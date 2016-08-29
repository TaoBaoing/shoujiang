using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace BasicFramework.Component
{
    /// <summary>
    /// 包装IQueryable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PowerfulQuery<T> : IQueryable<T>, IOrderedQueryable<T>, IOrderedQueryable
    {
        #region 包装IQueryable<T>

        PowerfulQueryProvider<T> _provider;
        /// <summary>
        /// 原始IQueryable
        /// </summary>
        IQueryable<T> _inner;

        internal IQueryable<T> InnerQuery { get { return _inner; } }

        internal PowerfulQuery(IQueryable<T> inner)
        {
            _inner = inner;
            _provider = new PowerfulQueryProvider<T>(this);
        }

        Expression IQueryable.Expression { get { return _inner.Expression; } }
        Type IQueryable.ElementType { get { return typeof(T); } }
        IQueryProvider IQueryable.Provider { get { return _provider; } }
        public IEnumerator<T> GetEnumerator() { return _inner.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return _inner.GetEnumerator(); }
        public override string ToString() { return _inner.ToString(); }

        #endregion

        
    }


}
