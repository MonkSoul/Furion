using Fur.DatabaseVisitor.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlDataSetQueryExtensions
    {
        public static DataSet SqlDataSetQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataSet = new DataSet();
            var (dbConnection, dbCommand, dbDataAdapter) = WrapperDbDataAdapter(databaseFacade, sql, parameters);
            dbDataAdapter.Fill(dataSet);
            dbConnection.Close();
            dbCommand.Parameters.Clear();
            return dataSet;
        }

        public static async Task<DataSet> SqlDataSetQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataSet = new DataSet();
            var (dbConnection, dbCommand, dbDataAdapter) = await WrapperDbDataAdapterAsync(databaseFacade, sql, parameters);
            dbDataAdapter.Fill(dataSet);
            await dbConnection.CloseAsync();
            dbCommand.Parameters.Clear();
            return dataSet;
        }

        public static IEnumerable<T1> SqlDataSetQuery<T1>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count == 0) return default;
            return dataset.Tables[0].ToEnumerable<T1>();
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>());
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default);
            }
            return default;
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>());
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default);
            }
            return default;
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>());
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default);
            }
            return default;
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>());
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default);
            }
            return default;
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 6)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>());
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default, default);
            }
            return default;
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 7)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), dataset.Tables[6].ToEnumerable<T7>());
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default, default, default);
            }
            return default;
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), dataset.Tables[6].ToEnumerable<T7>(), dataset.Tables[7].ToEnumerable<T8>());
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), dataset.Tables[6].ToEnumerable<T7>(), default);
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), default, default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), default, default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default, default, default, default);
            }
            return default;
        }

        public static object SqlDataSetQuery(this DatabaseFacade databaseFacade, string sql, object[] types, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]), dataset.Tables[5].ToEnumerable(types[5]), dataset.Tables[6].ToEnumerable(types[6]), dataset.Tables[7].ToEnumerable(types[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]), dataset.Tables[5].ToEnumerable(types[5]), dataset.Tables[6].ToEnumerable(types[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]), dataset.Tables[5].ToEnumerable(types[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToEnumerable(types[0]);
            }
            return default;
        }

        public static async Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count == 0) return default;
            return dataset.Tables[0].ToEnumerable<T1>();
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>());
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default);
            }
            return default;
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>());
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default);
            }
            return default;
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>());
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default);
            }
            return default;
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>());
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default);
            }
            return default;
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 6)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>());
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default, default);
            }
            return default;
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 7)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), dataset.Tables[6].ToEnumerable<T7>());
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default, default, default);
            }
            return default;
        }

        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), dataset.Tables[6].ToEnumerable<T7>(), dataset.Tables[7].ToEnumerable<T8>());
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), dataset.Tables[6].ToEnumerable<T7>(), default);
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), dataset.Tables[5].ToEnumerable<T6>(), default, default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), dataset.Tables[4].ToEnumerable<T5>(), default, default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), dataset.Tables[3].ToEnumerable<T4>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), dataset.Tables[2].ToEnumerable<T3>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), dataset.Tables[1].ToEnumerable<T2>(), default, default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToEnumerable<T1>(), default, default, default, default, default, default, default);
            }
            return default;
        }

        public static async Task<object> SqlDataSetQueryAsync(this DatabaseFacade databaseFacade, string sql, object[] types, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]), dataset.Tables[5].ToEnumerable(types[5]), dataset.Tables[6].ToEnumerable(types[6]), dataset.Tables[7].ToEnumerable(types[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]), dataset.Tables[5].ToEnumerable(types[5]), dataset.Tables[6].ToEnumerable(types[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]), dataset.Tables[5].ToEnumerable(types[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]), dataset.Tables[4].ToEnumerable(types[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]), dataset.Tables[3].ToEnumerable(types[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]), dataset.Tables[2].ToEnumerable(types[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToEnumerable(types[0]), dataset.Tables[1].ToEnumerable(types[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToEnumerable(types[0]);
            }
            return default;
        }

        #region 包装数据库连接、执行命令、适配器 -/* private static (DbConnection, DbCommand, DbDataAdapter) WrapperDbDataAdapter(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        /// <summary>
        /// 包装数据库连接、执行命令、适配器
        /// </summary>
        /// <param name="databaseFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static (DbConnection, DbCommand, DbDataAdapter) WrapperDbDataAdapter(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dbConnection = databaseFacade.GetDbConnection();
            var dbProviderFactory = DbProviderFactories.GetFactory(dbConnection);
            dbConnection = new ProfiledDbConnection(dbConnection, MiniProfiler.Current);
            var profiledDbProviderFactory = new ProfiledDbProviderFactory(dbProviderFactory, true);
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            var dbDataAdapter = profiledDbProviderFactory.CreateDataAdapter();
            dbCommand.CommandText = sql;
            Helper.FixedAndCombineSqlParameters(ref dbCommand, parameters);
            dbDataAdapter.SelectCommand = dbCommand;

            return (dbConnection, dbCommand, dbDataAdapter);
        }

        #endregion 包装数据库连接、执行命令、适配器 -/* private static (DbConnection, DbCommand, DbDataAdapter) WrapperDbDataAdapter(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        #region 包装数据库连接、执行命令、适配器 -/* private static async Task<(DbConnection, DbCommand, DbDataAdapter)> WrapperDbDataAdapterAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        /// <summary>
        /// 包装数据库连接、执行命令、适配器
        /// </summary>
        /// <param name="databaseFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static async Task<(DbConnection, DbCommand, DbDataAdapter)> WrapperDbDataAdapterAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dbConnection = databaseFacade.GetDbConnection();
            var dbProviderFactory = DbProviderFactories.GetFactory(dbConnection);
            dbConnection = new ProfiledDbConnection(dbConnection, MiniProfiler.Current);
            var profiledDbProviderFactory = new ProfiledDbProviderFactory(dbProviderFactory, true);
            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            var dbDataAdapter = profiledDbProviderFactory.CreateDataAdapter();
            dbCommand.CommandText = sql;
            Helper.FixedAndCombineSqlParameters(ref dbCommand, parameters);
            dbDataAdapter.SelectCommand = dbCommand;

            return await Task.FromResult((dbConnection, dbCommand, dbDataAdapter));
        }

        #endregion 包装数据库连接、执行命令、适配器 -/* private static async Task<(DbConnection, DbCommand, DbDataAdapter)> WrapperDbDataAdapterAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
    }
}