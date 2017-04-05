using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace FisherCommunity.App.Common
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="commandText"></param>
        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(SysConfig.ConnectionString, commandText);
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, string commandText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, conn);
                command.CommandTimeout = conn.ConnectionTimeout;
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandText"></param>
        public static int ExecuteNonQuery(IDbTransaction transaction, string commandText)
        {
            SqlTransaction sqlTransaction = transaction as SqlTransaction;
            if (sqlTransaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            SqlCommand command = new SqlCommand(commandText, sqlTransaction.Connection, sqlTransaction);
            command.CommandTimeout = sqlTransaction.Connection.ConnectionTimeout;
            return command.ExecuteNonQuery();
        }
        /// <summary>
        /// 取得单值
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(SysConfig.ConnectionString, commandText);
        }
        /// <summary>
        /// 取得单值
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, string commandText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, conn);
                command.CommandTimeout = conn.ConnectionTimeout;
                conn.Open();
                object returnValue = command.ExecuteScalar();
                return returnValue;
            }
        }
        /// <summary>
        /// 取得单值（事务）
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(IDbTransaction transaction, string commandText)
        {
            SqlTransaction sqlTransaction = transaction as SqlTransaction;
            if (sqlTransaction == null)
            {
                return null;
            }
            SqlCommand command = new SqlCommand(commandText, sqlTransaction.Connection, sqlTransaction);
            command.CommandTimeout = sqlTransaction.Connection.ConnectionTimeout;
            return command.ExecuteScalar();
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(SysConfig.ConnectionString, commandText);
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, string commandText)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(commandText, connectionString))
            {
                da.Fill(dt);
                return dt;
            }
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(IDbTransaction transaction, string commandText)
        {
            if (transaction == null)
            {
                return null;
            }
            SqlConnection con = (SqlConnection)transaction.Connection;
            SqlCommand command = new SqlCommand(commandText, con);
            command.CommandTimeout = con.ConnectionTimeout;
            command.Transaction = (SqlTransaction)transaction;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(SysConfig.ConnectionString, commandText);
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, string commandText)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(commandText, connectionString);
            da.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string tableName)
        {
            string commandText = string.Format("select * from [{0}]", tableName);
            return ExecuteDataTable(commandText);
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string tableName, string condition)
        {
            return GetDataTable(tableName, condition, null);
        }
        /// <summary>
        /// 取得数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string tableName, string condition, string orderBy)
        {
            string commandText = string.Format("select * from [{0}] {1} {2}", tableName, string.IsNullOrEmpty(condition) ? "" : " where " + condition, string.IsNullOrEmpty(orderBy) ? "" : " order by " + orderBy);
            return ExecuteDataTable(commandText);
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction()
        {
            return BeginTransaction(SysConfig.ConnectionString);
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            return transaction;
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        public static void CommitTransaction(IDbTransaction transaction)
        {
            SqlConnection con = (SqlConnection)transaction.Connection;
            transaction.Commit();
            con.Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction"></param>
        public static void RollbackTransaction(IDbTransaction transaction)
        {
            SqlConnection con = (SqlConnection)transaction.Connection;
            transaction.Rollback();
            con.Close();
        }
        /// <summary>
        /// 数据库是否在本地
        /// </summary>
        /// <param name="databaseServer"></param>
        /// <returns></returns>
        public static bool IsLocalDatabase(string databaseServer)
        {
            if (databaseServer.StartsWith("."))
            {
                return true;
            }
            if (databaseServer.ToLower().StartsWith("(local)"))
            {
                return true;
            }
            string serverAddress = databaseServer;
            if (databaseServer.Contains(@"\"))
            {
                serverAddress = serverAddress.Substring(0, serverAddress.IndexOf(@"\"));
            }
            if (serverAddress.ToLower() == Environment.MachineName.ToLower())
            {
                return true;
            }
            IPAddress[] ipAddressArray = Dns.GetHostAddresses(Environment.MachineName);
            foreach (IPAddress ipAddress in ipAddressArray)
            {
                if (ipAddress.ToString() == serverAddress)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="tableName"></param>
        public static void DropTable(IDbTransaction transaction, string tableName)
        {
            string commandText = string.Format(@"if exists (select * from sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
                drop table [{0}]",
                tableName);
            ExecuteNonQuery(transaction, commandText);
        }
        /// <summary>
        /// 取得指定字段最大整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int? GetMaxIntValue(string tableName, string fieldName)
        {
            return GetMaxIntValue(tableName, fieldName, null);
        }
        /// <summary>
        /// 取得指定字段最大整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int? GetMaxIntValue(string tableName, string fieldName, string condition)
        {
            string commandText = string.Format("select max([{0}]) from [{1}]", fieldName, tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = string.Format("select max([{0}]) from [{1}] where {2}", fieldName, tableName, condition);
            }
            object objValue = DBHelper.ExecuteScalar(commandText);
            if (objValue == DBNull.Value || objValue == null)
            {
                return null;
            }
            try
            {
                int value = Convert.ToInt32(objValue);
                return value;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 取得指定字段最大长整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static long? GetMaxLongValue(string tableName, string fieldName)
        {
            return GetMaxLongValue(tableName, fieldName, null);
        }
        /// <summary>
        /// 取得指定字段最大长整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static long? GetMaxLongValue(string tableName, string fieldName, string condition)
        {
            string commandText = string.Format("select max([{0}]) from [{1}]", fieldName, tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = string.Format("select max([{0}]) from [{1}] where {2}", fieldName, tableName, condition);
            }
            object objValue = DBHelper.ExecuteScalar(commandText);
            if (objValue == DBNull.Value || objValue == null)
            {
                return null;
            }
            try
            {
                long value = Convert.ToInt64(objValue);
                return value;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 取得指定字段最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetMaxValue(string tableName, string fieldName)
        {
            return GetMaxValue(tableName, fieldName, null);
        }
        /// <summary>
        /// 取得指定字段最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetMaxValue(string tableName, string fieldName, string condition)
        {
            string commandText = string.Format("select max(dbo.PadLeft([{0}], '0', 50)) from [{1}]", fieldName, tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = string.Format("select max(dbo.PadLeft([{0}], '0', 50)) from [{1}] where {2}", fieldName, tableName, condition);
            }
            object objValue = DBHelper.ExecuteScalar(commandText);
            if (objValue == DBNull.Value || objValue == null)
            {
                return null;
            }
            string value = objValue.ToString();
            return value;
        }
        /// <summary>
        /// 取得指定字段最大整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static int? GetMaxIntValue(IDbTransaction transaction, string tableName, string fieldName)
        {
            return GetMaxIntValue(transaction, tableName, fieldName, null);
        }
        /// <summary>
        /// 取得指定字段最大整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int? GetMaxIntValue(IDbTransaction transaction, string tableName, string fieldName, string condition)
        {
            string commandText = string.Format("select max([{0}]) from [{1}]", fieldName, tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = string.Format("select max([{0}]) from [{1}] where {2}", fieldName, tableName, condition);
            }
            object objValue = DBHelper.ExecuteScalar(transaction, commandText);
            if (objValue == DBNull.Value || objValue == null)
            {
                return null;
            }
            try
            {
                int value = Convert.ToInt32(objValue);
                return value;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 取得指定字段最大长整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static long? GetMaxLongValue(IDbTransaction transaction, string tableName, string fieldName)
        {
            return GetMaxLongValue(transaction, tableName, fieldName, null);
        }
        /// <summary>
        /// 取得指定字段最大长整数值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static long? GetMaxLongValue(IDbTransaction transaction, string tableName, string fieldName, string condition)
        {
            string commandText = string.Format("select max([{0}]) from [{1}]", fieldName, tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = string.Format("select max([{0}]) from [{1}] where {2}", fieldName, tableName, condition);
            }
            object objValue = DBHelper.ExecuteScalar(transaction, commandText);
            if (objValue == DBNull.Value || objValue == null)
            {
                return null;
            }
            try
            {
                long value = Convert.ToInt64(objValue);
                return value;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 取得指定字段最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetMaxValue(IDbTransaction transaction, string tableName, string fieldName)
        {
            return GetMaxValue(transaction, tableName, fieldName, null);
        }
        /// <summary>
        /// 取得指定字段最大值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string GetMaxValue(IDbTransaction transaction, string tableName, string fieldName, string condition)
        {
            string commandText = string.Format("select max(dbo.PadLeft([{0}], '0', 50)) from [{1}]", fieldName, tableName);
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = string.Format("select max(dbo.PadLeft([{0}], '0', 50)) from [{1}] where {2}", fieldName, tableName, condition);
            }
            object objValue = DBHelper.ExecuteScalar(transaction, commandText);
            if (objValue == DBNull.Value || objValue == null)
            {
                return null;
            }
            string value = objValue.ToString();
            return value;
        }
        /// <summary>
        /// 生成新编码
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string NewCode(string tableName, string fieldName)
        {
            return NewCode(tableName, fieldName, null);
        }
        /// <summary>
        /// 生成新编码
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string NewCode(string tableName, string fieldName, string condition)
        {
            string maxValue = GetMaxValue(tableName, fieldName, condition);
            if (maxValue == null)
            {
                return "1";
            }
            string number = "";
            string prefix = "";
            for (int i = 0; i < maxValue.Length; i++)
            {
                char currentChar = maxValue[i];
                if (Char.IsDigit(currentChar))
                {
                    number += currentChar;
                }
                else
                {
                    prefix += currentChar;
                    number = "";
                }
            }
            if (number == "")
            {
                return prefix + "1";
            }
            string newCode = prefix + Convert.ToString(Convert.ToInt64(number) + 1);
            return newCode;
        }
    }
}
