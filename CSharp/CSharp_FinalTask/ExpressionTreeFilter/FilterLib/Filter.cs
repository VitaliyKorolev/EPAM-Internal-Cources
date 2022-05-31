using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FilterLib
{
    [Serializable]
    public class Filter<T>
    {
        private Expression conditions = null;
        private ParameterExpression param = Expression.Parameter(typeof(T), "el");
        public void PropertyEqualsToValue<TProp>(string propertyName, TProp value)
        {
            ConstantExpression constant = Expression.Constant(value, typeof(TProp));
            ConstantExpression zero = Expression.Constant(0, typeof(int));
            AddCondition(propertyName, constant, zero);
        }

        public void PropertyGreaterThanValue<TProp>(string propertyName, TProp value)
        {
            ConstantExpression constant = Expression.Constant(value, typeof(TProp));
            ConstantExpression one = Expression.Constant(1, typeof(int));
            AddCondition(propertyName, constant, one);
        }

        public void PropertyLessThanValue<TProp>(string propertyName, TProp value)
        {
            ConstantExpression constant = Expression.Constant(value, typeof(TProp));
            ConstantExpression minusOne = Expression.Constant(-1, typeof(int));
            AddCondition(propertyName, constant, minusOne);
        }
        public void PropertyInRange<TProp>(string propertyName, TProp lowerBoundValue, TProp upperBoundValue)
        {
            ConstantExpression lowerBound = Expression.Constant(lowerBoundValue, typeof(TProp));
            ConstantExpression upperBound = Expression.Constant(upperBoundValue, typeof(TProp));
            ConstantExpression minusOne = Expression.Constant(-1, typeof(int));
            ConstantExpression one = Expression.Constant(1, typeof(int));
            AddCondition(propertyName, upperBound, minusOne, true);
            AddCondition(propertyName, lowerBound, one, true);
        }

        public void PropertyContainsString(string propertyName, string str)
        {
            Type typeOfT = typeof(T);
            PropertyInfo propertyOfT = typeOfT.GetProperty(propertyName);
            if (propertyOfT?.PropertyType != typeof(string))
            {
                throw new ArgumentException($"Type of property isn't string");
            }

            MemberExpression valueInProperty = Expression.MakeMemberAccess(param, propertyOfT);
            MethodInfo method = typeof(string).GetMethods()
                .Where(m => m.Name == "Contains" && m.GetParameters().Length == 1).FirstOrDefault();
            ConstantExpression constant = Expression.Constant(str, typeof(string));
            MethodCallExpression condition = Expression.Call(valueInProperty, method, constant);
            if (conditions == null)
                conditions = condition;
            else
                conditions = Expression.And(conditions, condition);
        }

        public IEnumerable<T> Apply(IEnumerable<T> collection)
        {
            if(conditions != null)
            {
                Expression<Func<T, bool>> whereExpression = Expression<Func<T, bool>>.Lambda<Func<T, bool>>(conditions, param);
                return collection.Where(whereExpression.Compile()).ToList();
            }
            return collection;
        }

        private void AddCondition(string propertyName, ConstantExpression constant, ConstantExpression number, bool includeBound = false)
        {
            Type typeOfT = typeof(T);
            PropertyInfo propertyOfT = typeOfT.GetProperty(propertyName);
            if(propertyOfT == null)
            {
                throw new ArgumentException($"Type {typeOfT.FullName} doesn't contain property {propertyName}");
            }
            if (propertyOfT.PropertyType != constant.Type)
            {
                throw new ArgumentException($"Type of property isn't equal to constant type");
            }

            MemberExpression valueInProperty = Expression.MakeMemberAccess(param, propertyOfT);
            MethodInfo method = propertyOfT.PropertyType.GetMethod("CompareTo", new Type[] { propertyOfT.PropertyType } );
            MethodCallExpression methodCallExpression = Expression.Call(valueInProperty, method, constant);
            BinaryExpression condition = null;
            if (!includeBound)
                condition = Expression.Equal(methodCallExpression, number);
            else
            {
                BinaryExpression temp = Expression.Equal(methodCallExpression, number);
                condition = Expression.Or(temp, Expression.Equal(methodCallExpression, Expression.Constant(0, typeof(int))));
            }
            if (conditions == null)
                conditions = condition;
            else
                conditions = Expression.And(conditions, condition);
        }
    }
}
