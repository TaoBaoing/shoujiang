using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BasicFramework.Component
{
    /// <summary>
    /// 拓展方法
    /// </summary>
    public static class PowerfulExtensions
    {
        public static IQueryable<T> AsPowerful<T>(this IQueryable<T> query)
        {
            if (query is PowerfulQuery<T>)
            {
                return (PowerfulQuery<T>)query;
            }
            return new PowerfulQuery<T>(query);
        }

        #region Power
        public static Expression Power(this Expression expr)
        {
            return new PowerfulExpressionVisitor().Visit(expr);
        }

        public static Expression<TDelegate> Power<TDelegate>(this Expression<TDelegate> expr)
        {
            return (Expression<TDelegate>)new PowerfulExpressionVisitor().Visit(expr);
        }

        #endregion

        #region Invoke
        public static TResult Invoke<TResult>(this Expression<Func<TResult>> expr)
        {
            return expr.Compile()();
        }

        public static TResult Invoke<T1, TResult>(this Expression<Func<T1, TResult>> expr, T1 arg1)
        {
            return expr.Compile()(arg1);
        }

        public static TResult Invoke<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> expr, T1 arg1, T2 arg2)
        {
            return expr.Compile()(arg1, arg2);
        }

        public static TResult Invoke<T1, T2, T3, TResult>(this Expression<Func<T1, T2, T3, TResult>> expr, T1 arg1, T2 arg2, T3 arg3)
        {
            return expr.Compile()(arg1, arg2, arg3);
        }

        public static TResult Invoke<T1, T2, T3, T4, TResult>(this Expression<Func<T1, T2, T3, T4, TResult>> expr, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return expr.Compile()(arg1, arg2, arg3, arg4);
        }
        #endregion

        #region 表达式构建
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            InvocationExpression right = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, right), expr1.Parameters);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            InvocationExpression right = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, right), expr1.Parameters);
        }
        #endregion

        #region 自定义投影、按需查询、获得扁平化的对象。
        /// <summary>
        ///根据T的属性来构建Select语句，查询完直接映射成T。（只查询传入的属性，自动搜索复杂属性）
        /// </summary>
        /// <typeparam name="T">返回元素类型</typeparam>
        /// <param name="source"></param>
        /// <returns>IQueryable<T></returns>
        public static IQueryable<T> Select<T>(this IQueryable source)
        {
            if (!source.GetType().IsGenericType)
            {
                throw new ArgumentException("必须是泛型IQueryable<T>");
            }

            var sourceArgumentType = source.GetType().GetGenericArguments()[0];

            ParameterExpression lambdaParameter = Expression.Parameter(sourceArgumentType, "p");
            var sourceMembers = sourceArgumentType.GetProperties();
            var destinationMembers = typeof(T).GetProperties();

            List<MemberBinding> bindings = new List<MemberBinding>();

            foreach (var dest in destinationMembers)
            {
                var topPrimitiveResult = sourceMembers.Where(i =>
                    !i.PropertyType.IsNested &&
                     //(i.PropertyType.IsPrimitive || i.PropertyType == typeof(string)) &&
                    i.PropertyType.IsPublic &&
                    i.Name == dest.Name &&
                    i.PropertyType == dest.PropertyType
                    );
                //简单类型
                if (topPrimitiveResult.Count() > 0)
                {
                    var binding = Expression.Bind(dest, Expression.Property(lambdaParameter, topPrimitiveResult.FirstOrDefault()));
                    bindings.Add(binding);
                }
                else
                {
                    //复杂类型搜索
                    var topComplexPropertyResult = sourceMembers.Where(i =>
                         !i.PropertyType.IsNested &&
                         //!(i.PropertyType.IsPrimitive || i.PropertyType == typeof(string)) &&
                          dest.Name.StartsWith(i.PropertyType.Name) &&
                         i.PropertyType.IsPublic
                        );
                    if (topComplexPropertyResult.Count() > 0)
                    {
                        var topComplexProperty = topComplexPropertyResult.FirstOrDefault();
                        string secPrimitivePropertyName = dest.Name.Replace(topComplexProperty.PropertyType.Name, "").Trim();

                        var secPrimitivePropertyResult = topComplexProperty.PropertyType.GetProperties().Where(i =>
                               !i.PropertyType.IsNested &&
                                //(i.PropertyType.IsPrimitive || i.PropertyType == typeof(string)) &&
                               i.PropertyType.IsPublic &&
                               i.Name == secPrimitivePropertyName &&
                               i.PropertyType == dest.PropertyType
                            );
                        if (secPrimitivePropertyResult.Count() > 0)
                        {
                            var oneExpression = Expression.Property(lambdaParameter, topComplexProperty);
                            var twoExpression = Expression.Property(oneExpression, secPrimitivePropertyResult.FirstOrDefault());
                            var binding = Expression.Bind(dest, twoExpression);
                            bindings.Add(binding);
                        }
                    }

                }
            }

            var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            var lambda = Expression.Lambda(body, lambdaParameter);

            //由于泛型重载的参数难以指定，暂且获取所有方法后筛选
            var selectMethod = typeof(Queryable).GetMethods().Where(i => i.Name == "Select").ElementAt(0);
            //创建泛型方法
            var genericSelectMethod = selectMethod.MakeGenericMethod(sourceArgumentType, typeof(T));
            //构造方法调用的表达式目录树
            var newCall = Expression.Call(null, genericSelectMethod, Expression.Constant(source), lambda);

            var result = source.Provider.CreateQuery<T>(newCall);

            return result;
        }


        #endregion

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T local in source)
            {
                action(local);
            }
        }
    }
}

