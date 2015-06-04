using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleXmlSerializer.Utils
{
    internal class ExpressionUtils
    {
        public static Func<object> GetFactory(Type type)
        {
            var newExpression = Expression.New(type);
            if(!type.IsValueType)
                return Expression.Lambda<Func<object>>(newExpression).Compile();

            var conversionExpression = Expression.Convert(newExpression, typeof(object));
            return Expression.Lambda<Func<object>>(conversionExpression).Compile();
        }

        public static Func<object, object> GetPropertyGetter(PropertyInfo propertyInfo)
        {
            var paramExpr = Expression.Parameter(typeof(object), "arg");
            var instanceExpr = Expression.Convert(paramExpr, propertyInfo.DeclaringType);
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