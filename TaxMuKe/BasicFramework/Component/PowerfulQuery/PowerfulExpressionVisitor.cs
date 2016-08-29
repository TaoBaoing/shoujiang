using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BasicFramework.Component
{
    internal class PowerfulExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, Expression> _replaceVars;

        internal PowerfulExpressionVisitor()
        {
        }

        private PowerfulExpressionVisitor(Dictionary<ParameterExpression, Expression> replaceVars)
        {
            this._replaceVars = replaceVars;
        }

        private Expression TryVisitExpressionFunc(MemberExpression input, FieldInfo field)
        {
            var prope = input.Member as PropertyInfo;
            if ((field.FieldType.IsSubclassOf(typeof(Expression))) ||
                (prope != null && prope.PropertyType.IsSubclassOf(typeof(Expression))))
                return Visit(Expression.Lambda<Func<Expression>>(input).Compile()());

            return input;
        }

        protected override Expression VisitInvocation(InvocationExpression iv)
        {
            var target = iv.Expression;
            if (target is MemberExpression) target = TransformExpr((MemberExpression)target);
            if (target is ConstantExpression) target = ((ConstantExpression)target).Value as Expression;

            var lambda = (LambdaExpression)target;

            var replaceVars = _replaceVars == null ?
                new Dictionary<ParameterExpression, Expression>()
                : new Dictionary<ParameterExpression, Expression>(_replaceVars);

            try
            {
                for (int i = 0; i < lambda.Parameters.Count; i++)
                    replaceVars.Add(lambda.Parameters[i], iv.Arguments[i]);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("不允许递归", ex);
            }
            //参数统一化
            return new PowerfulExpressionVisitor(replaceVars).Visit(lambda.Body);
          
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            return m.Member.DeclaringType.Name.StartsWith("<>") ?
                 TransformExpr(m)
                 : base.VisitMember(m);
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.Name == "Invoke" && m.Method.DeclaringType == typeof(PowerfulExtensions))
            {
                var target = m.Arguments[0];
                if (target is MemberExpression) target = TransformExpr((MemberExpression)target);
                if (target is ConstantExpression) target = ((ConstantExpression)target).Value as Expression;

                var lambda = (LambdaExpression)target;

                var replaceVars = _replaceVars == null ?
                    new Dictionary<ParameterExpression, Expression>()
                    : new Dictionary<ParameterExpression, Expression>(_replaceVars);

                try
                {
                    for (int i = 0; i < lambda.Parameters.Count; i++)
                        replaceVars.Add(lambda.Parameters[i], m.Arguments[i + 1]);
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidOperationException("不允许递归", ex);
                }

                return new PowerfulExpressionVisitor(replaceVars).Visit(lambda.Body);
            }

            // 拦截Compile，避免真正编译
            if (m.Method.Name == "Compile" && m.Object is MemberExpression)
            {
                var me = (MemberExpression)m.Object;
                var newExpr = TransformExpr(me);
                if (newExpr != me) return newExpr;
            }

            if (m.Method.Name == "AsPowerful" && m.Method.DeclaringType == typeof(PowerfulExtensions))
                return m.Arguments[0];

            return base.VisitMethodCall(m);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return (_replaceVars != null) && (_replaceVars.ContainsKey(p)) ? _replaceVars[p] : base.VisitParameter(p);
        }

        private Expression TransformExpr(MemberExpression input)
        {
            if (input == null)
                return null;

            var field = input.Member as FieldInfo;

            if (field == null) return input;

            // Collapse captured outer variables
            if (!input.Member.ReflectedType.IsNestedPrivate
                || !input.Member.ReflectedType.Name.StartsWith("<>")) // captured outer variable
            {
                return TryVisitExpressionFunc(input, field);
            }

            var expression = input.Expression as ConstantExpression;
            if (expression != null)
            {
                var obj = expression.Value;
                if (obj == null) return input;
                var t = obj.GetType();
                if (!t.IsNestedPrivate || !t.Name.StartsWith("<>")) return input;
                var fi = (FieldInfo)input.Member;
                var result = fi.GetValue(obj);
                var exp = result as Expression;
                if (exp != null) return Visit(exp);
            }

            return TryVisitExpressionFunc(input, field);
        }
    }
}

