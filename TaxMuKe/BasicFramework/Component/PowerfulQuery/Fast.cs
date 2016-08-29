using System;
using System.Linq.Expressions;

namespace BasicFramework.Component
{
    /// <summary>
    /// 表达式目录树\泛型委托 快速构建器
    /// </summary>
    public static class Fast
    {
        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }
        public static Expression<Func<TResult>> Expression<TResult>(Expression<Func<TResult>> expr)
        {
            return expr;
        }

        public static Expression<Func<T, TResult>> Expression<T, TResult>(Expression<Func<T, TResult>> expr)
        {
            return expr;
        }

        public static Expression<Func<T1, T2, TResult>> Expression<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expr)
        {
            return expr;
        }

        public static Func<TResult> Func<TResult>(Func<TResult> expr)
        {
            return expr;
        }

        public static Func<T, TResult> Func<T, TResult>(Func<T, TResult> expr)
        {
            return expr;
        }

        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> expr)
        {
            return expr;
        }

    }
}

