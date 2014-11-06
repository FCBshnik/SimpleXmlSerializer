using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleXmlSerializer.Utils
{
    public class ExpressionUtils
    {
        public static Func<object> GetFactory(Type type)
        {
            var newExpression = Expression.New(type);
            return Expression.Lambda<Func<object>>(newExpression).Compile();
        }

        public static Func<object, object> GetPropertyGetter(PropertyInfo propertyInfo)
        {
            var paramExpr = Expression.Parameter(typeof(object), "arg");
// ReSharper disable AssignNullToNotNullAttribute
            var instanceExpr = Expression.Convert(paramExpr, propertyInfo.DeclaringType);
// ReSharper restore AssignNullToNotNullAttribute
            Expression callExpression = Expression.Call(instanceExpr, propertyInfo.GetGetMethod());

            if (propertyInfo.PropertyType.IsValueType)
            {
                callExpression = Expression.Convert(callExpression, typeof(object));
            }

            var lambda = Expression.Lambda<Func<object, object>>(callExpression, paramExpr);
            return lambda.Compile();
        }
    }
}