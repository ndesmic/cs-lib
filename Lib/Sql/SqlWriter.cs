using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Lib.Sql
{
    public static class SqlWriter
    {
        public static string GetSql(SqlCommand command, bool isVerbose = false)
        {
            var sql = string.Empty;
            if (isVerbose)
            {
                sql += $"--server: {command.Connection.DataSource}\n";
                sql += $"use {command.Connection.Database};\n";
            }
            if (command.CommandType == CommandType.StoredProcedure)
            {
                sql += $"exec {command.CommandText}\n";
                sql = command.Parameters.Cast<SqlParameter>().Aggregate(sql, (current, param) => current + FormatSqlParamExpression(param) + ",\n");
            }
            return sql.Trim('\n');
        }

        public static string FormatSqlParamExpression(SqlParameter sqlParameter)
        {
            var paramBuilder = new StringBuilder();

            if (sqlParameter.Direction == ParameterDirection.Output)
            {
                paramBuilder.Append($"{sqlParameter.ParameterName}={sqlParameter.ParameterName} output");
            }
            else
            {
                paramBuilder.Append($"{sqlParameter.ParameterName}={FormatSqlParamValue(sqlParameter)}");
            }

            return paramBuilder.ToString();
        }

        public static string FormatSqlParamValue(SqlParameter sqlParameter)
        {
            if (sqlParameter.Value == null)
            {
                return "NULL";
            }

            switch (sqlParameter.SqlDbType)
            {
                case SqlDbType.NVarChar:
                case SqlDbType.VarChar:
                case SqlDbType.DateTime:
                    return $"'{sqlParameter.Value.ToString().Replace("'", "''")}'";
                case SqlDbType.Bit:
                    return ((bool)sqlParameter.Value) ? "1" : "0";
                default:
                    return sqlParameter.Value.ToString().Replace("'", "''");
            }
        }
    }
}
