using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlExecutedExtensions
    {
        public static SqlParameter[] ToSqlParameters<TParameterModel>(this TParameterModel parameterModel) where TParameterModel : class
        {
            var type = parameterModel.GetType();
            var properities = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var paramValues = new List<SqlParameter>();

            for (int i = 0; i < properities.Length; i++)
            {
                var property = properities[i];
                var value = property.GetValue(parameterModel);
                if (value == null) continue;

                paramValues.Add(new SqlParameter(property.Name, value));
            }
            return paramValues.ToArray();
        }
    }
}
