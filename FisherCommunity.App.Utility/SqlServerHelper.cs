using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace FisherCommunity.App.Utility
{
    /// <summary>
    /// SQLServer帮助类
    /// </summary>
    public class SqlServerHelper
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static void AddUser(string connectionString, string userName, string password)
        {
            string commandText = "exec sp_addlogin '" + userName + "','" + password + "'";
            ExecuteNonQuery(connectionString, commandText);
        }
        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="databaseName">要附加到服务器的数据库的名称。该名称必须是唯一的。</param>
        /// <param name="fileNames">数据库文件的物理名称，包括路径。最多可以指定 16 个文件名。</param>
        /// <returns></returns>
        public static void AttachDatabase(string connectionString, string databaseName, string[] fileNames)
        {
            string commandText = string.Format("EXEC sp_attach_db '{0}'", databaseName);
            foreach (string fileName in fileNames)
            {
                commandText += string.Format(", '{0}'", fileName);
            }
            ExecuteNonQuery(connectionString, commandText);
        }
        /// <summary>
        /// 分离数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="skipChecks"></param>
        /// <returns></returns>
        public static void DetachDatabase(string connectionString, string databaseName, bool skipChecks)
        {
            string commandText = string.Format("EXEC sp_detach_db '{0}', '{1}'", databaseName, skipChecks);
            ExecuteNonQuery(connectionString, commandText);
        }
        /// <summary>
        /// 检查数据库是否存在
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static bool ExistsDatabase(string connectionString, string databaseName)
        {
            string commandText = string.Format("use master \n select count(*) from sysdatabases where name = '{0}'", databaseName);
            int count = Convert.ToInt32(ExecuteScalar(connectionString, commandText));
            return count != 0;

        }
        /// <summary>
        /// 取得单值
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connnectionString, string commandText)
        {
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                SqlCommand command = new SqlCommand(commandText, con);
                con.Open();
                object returnValue = command.ExecuteScalar();
                return returnValue;
            }
        }
        /// <summary>
        /// 执行语句返回数据集
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, string commandText)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, connectionString);
            DataSet dsResult = new DataSet();
            dataAdapter.Fill(dsResult);
            return dsResult;
        }
        /// <summary>
        /// 执行语句返回数据表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, string commandText)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, connectionString);
            DataTable dtResult = new DataTable();
            dataAdapter.Fill(dtResult);
            return dtResult;
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, string commandText)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(commandText, con);
                sqlCommand.CommandTimeout = GetConnectionTimeout();
                sqlCommand.CommandType = CommandType.Text;
                try
                {
                    con.Open();
                    int returnValue = sqlCommand.ExecuteNonQuery();
                    return returnValue;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="databaseName">数据库名</param>
        /// <returns></returns>
        public static void DropDatabase(string connectionString, string databaseName)
        {
            string commandText = string.Format("drop database {0}", databaseName);
            ExecuteNonQuery(connectionString, commandText);
        }
        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="backupDatabaseFilePath">备件文件路径</param>
        /// <param name="databaseLogicalDataName">数据文件逻辑名</param>
        /// <param name="databaseDataFilePath">数据文件物理路径</param>
        /// <param name="databaseLogicalLogName">日志文件逻辑名</param>
        /// <param name="databaseLogFilePath">日志文件物理路径</param>
        /// <returns></returns>
        public static void RestoreDatabase(string connectionString, string databaseName, string backupDatabaseFilePath, string databaseLogicalDataName, string databaseDataFilePath, string databaseLogicalLogName, string databaseLogFilePath)
        {
            string restoreCommandText = string.Format(@"RESTORE DATABASE {0} FROM DISK='{1}' WITH MOVE '{2}' TO '{3}',MOVE '{4}' TO '{5}',replace", databaseName, backupDatabaseFilePath, databaseLogicalDataName, databaseDataFilePath, databaseLogicalLogName, databaseLogFilePath);
            ExecuteNonQuery(connectionString, restoreCommandText);
        }
        /// <summary>
        /// 重命名数据库逻辑文件名。
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="oldName">当前逻辑文件名</param>
        /// <param name="newName">新逻辑文件名</param>
        /// <returns></returns>
        public static void RenameLogicalFileName(string connectionString, string databaseName, string oldName, string newName)
        {
            string renameCommandText = string.Format("ALTER DATABASE {0} MODIFY FILE ( NAME = '{1}', NEWNAME = '{2}'", databaseName, oldName, newName);
            ExecuteNonQuery(connectionString, renameCommandText);
        }
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="backupFilePath"></param>
        /// <returns></returns>
        public static void BackupDatabase(string connectionString, string databaseName, string backupFilePath)
        {
            string backupCommandText = string.Format("backup database {0} to disk='{1}' WITH INIT", databaseName, backupFilePath);
            ExecuteNonQuery(connectionString, backupCommandText);
        }
        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="backupFilePath"></param>
        /// <returns></returns>
        public static void RestoreDatabase(string connectionString, string databaseName, string backupFilePath)
        {
            string restoreCommandText = string.Format("restore database {0} from disk='{1}'", databaseName, backupFilePath);
            ExecuteNonQuery(connectionString, restoreCommandText);
        }
        /// <summary>
        /// 清除数据库连接
        /// </summary>
        /// <param name="masterConnectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static void ClearDatabaseConnection(string masterConnectionString, string databaseName)
        {
            DataTable dtWho = ExecuteDataTable(masterConnectionString, "sp_who");
            if (dtWho == null)
            {
                return;
            }
            //取得连接到指定数据库的系统进程列表
            List<string> spidList = new List<string>();
            foreach (DataRow dr in dtWho.Rows)
            {
                if (dr["dbname"] == DBNull.Value)
                {
                    continue;
                }
                string dbname = dr["dbname"].ToString();
                if (dbname == databaseName)
                {
                    string spid = dr["spid"].ToString();
                    spidList.Add(spid);
                }
            }
            //终止进程
            foreach (string spid in spidList)
            {
                string killCommandText = string.Format("kill {0}", spid);
                ExecuteNonQuery(masterConnectionString, killCommandText);
            }
        }
        /// <summary>
        /// 取得数据库连接字符串
        /// </summary>
        /// <param name="SSPIEnabled">是否集成验证</param>
        /// <param name="server">服务器</param>
        /// <param name="databaseName">数据库</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string GetConnectionString(bool SSPIEnabled, string server, string databaseName, string userName, string password)
        {
            int connectionTimeout = GetConnectionTimeout();
            return GetConnectionString(SSPIEnabled, server, databaseName, userName, password, connectionTimeout);
        }
        /// <summary>
        /// 取得数据库连接字符串
        /// </summary>
        /// <param name="SSPIEnabled">是否集成验证</param>
        /// <param name="server">服务器</param>
        /// <param name="databaseName">数据库</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string GetConnectionString(bool SSPIEnabled, string server, string databaseName, string userName, string password, int connectionTimeout)
        {
            string sRet = "";
            if (!SSPIEnabled)
            {
                if (null == server || server.Trim().Length <= 0 ||
                    null == userName || userName.Trim().Length <= 0 ||
                    null == databaseName || databaseName.Trim().Length <= 0)
                {
                    return null;
                }
                sRet = string.Format("DATA SOURCE={0};INITIAL CATALOG={1};user id={2};password={3};Connect Timeout={4}", server, databaseName, userName, password, connectionTimeout);
            }
            else
            {
                if (null == server || server.Trim().Length <= 0 ||
                    null == databaseName || databaseName.Trim().Length <= 0)
                {
                    return null;
                }
                sRet = string.Format("DATA SOURCE={0}; INITIAL CATALOG={1};Integrated Security=SSPI;Connect Timeout={2}", server, databaseName, connectionTimeout);
            }
            return sRet;
        }
        private static int GetConnectionTimeout()
        {
            if (ConfigurationManager.AppSettings["connectionTimeout"] != null)
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["connectionTimeout"]);
            }
            return 60;
        }
        /// <summary>
        /// 执行脚本文件
        /// </summary>
        /// <param name="trustedConnection"></param>
        /// <param name="server"></param>
        /// <param name="databaseName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="scriptFile"></param>
        /// <param name="outputFile"></param>
        public static void ExecuteScript(bool trustedConnection, string server, string databaseName, string userName, string password, string scriptFile, string outputFile)
        {
            string arguments = string.Empty;
            if (trustedConnection)
            {
                arguments = string.Format("-S {0} -d {1} -E -b -i \"{2}\" -o \"{3}\"", server, databaseName, scriptFile, outputFile);
            }
            else
            {
                arguments = string.Format("-S {0} -d {1} -b -U {2} -P {3} -i \"{4}\" -o \"{5}\"", server, databaseName, userName, password, scriptFile, outputFile);
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "osql";
            startInfo.Arguments = arguments;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TestConnection(string connectionString)
        {
            if (connectionString == null)
            {
                return false;
            }
            SqlConnection oCon = null;
            try
            {
                oCon = new SqlConnection(connectionString);
                oCon.Open();
                return true;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (null != oCon)
                {
                    oCon.Close();
                }
            }
        }
        /// <summary>
        /// 收缩日志
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        public static void DumpLog(string connectionString, string databaseName)
        {
            string backupCommandText = string.Format("BACKUP LOG [{0}] WITH NO_LOG", databaseName);
            string dumpCommandText = string.Format("DUMP TRANSACTION [{0}] WITH NO_LOG ", databaseName);
            string shrinkCommandText = string.Format("DBCC SHRINKDATABASE({0})", databaseName);
            ExecuteNonQuery(connectionString, backupCommandText);
            ExecuteNonQuery(connectionString, dumpCommandText);
            ExecuteNonQuery(connectionString, shrinkCommandText);
        }
        /// <summary>
        /// 重建索引
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        public static void DBReIndex(string connectionString, string databaseName)
        {
            //取得所有用户表
            List<string> tables = GetUserTables(connectionString);
            //重建索引
            foreach (string tableName in tables)
            {
                ReIndexTable(connectionString, tableName);
            }
        }
        /// <summary>
        /// 重建指定表索引
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        private static void ReIndexTable(string connectionString, string tableName)
        {
            string commandText = string.Format("DBCC DBREINDEX('{0}','',90)", tableName);
            ExecuteNonQuery(connectionString, commandText);
        }
        /// <summary>
        /// 检查数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        public static void CheckDB(string masterConnectionString, string databaseName)
        {
            //清楚数据连接
            ClearDatabaseConnection(masterConnectionString, databaseName);
            string setSingleUserModeCommmandText = string.Format("EXEC sp_dboption '{0}', 'single user', 'true'", databaseName);
            string checkDBCommandText = string.Format("dbcc checkdb ('{0}',REPAIR_ALLOW_DATA_LOSS)", databaseName);
            string setMultiUserModeCommmandText = string.Format("EXEC sp_dboption '{0}', 'single user', 'false'", databaseName);
            ExecuteNonQuery(masterConnectionString, setSingleUserModeCommmandText);
            ExecuteNonQuery(masterConnectionString, checkDBCommandText);
            ExecuteNonQuery(masterConnectionString, setMultiUserModeCommmandText);
        }
        /// <summary>
        /// 取得所有用户表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<string> GetUserTables(string connectionString)
        {
            string commandText = "select * from sysobjects where xtype='u' and name <> 'dtproperties'";
            DataTable dtTable = ExecuteDataTable(connectionString, commandText);
            List<string> tableList = new List<string>();
            foreach (DataRow drCurrent in dtTable.Rows)
            {
                tableList.Add(drCurrent["name"].ToString());
            }
            return tableList;
        }
        /// <summary>
        /// 取得数据库列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataTable GetDatabases(string connectionString)
        {
            string commandText = "exec sp_helpdb";
            return ExecuteDataTable(connectionString, commandText);

        }
        /// <summary>
        /// 取得所有表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataTable GetTables(string connectionString)
        {
            string commandText = "select * from sysobjects where xtype='u' and name <> 'dtproperties' order by name";
            return ExecuteDataTable(connectionString, commandText);
        }
        /// <summary>
        /// 取得所有表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataTable GetViews(string connectionString)
        {
            string commandText = "select * from sysobjects where xtype='v' and substring([name], 1, 3) <> 'sys'";
            return ExecuteDataTable(connectionString, commandText);
        }
        /// <summary>
        /// 取得所有表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataTable GetProcedures(string connectionString)
        {
            string commandText = "select * from sysobjects where xtype='p' and substring([name], 1,3) <> 'dt_'";
            return ExecuteDataTable(connectionString, commandText);
        }
        /// <summary>
        /// 取得指定表的外键字段和关联外键表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetForeignKeyTable(string connectionString, string tableName)
        {
            string commandText = string.Format(@"
                select distinct t1.fTableName,t1.name as fName,t2.rTableName,t2.name as rName
                from
                (select OBJECT_NAME(f.fkeyid) as ftableName, col.name, f.constid as temp
                 from syscolumns col,sysforeignkeys f
                 where f.fkeyid=col.id
                 and f.fkey=col.colid
                 and f.constid in
                 ( select distinct(id) 
                   from sysobjects
                   where OBJECT_NAME(parent_obj)='{0}'
                   and xtype='F'
                  )
                 ) as t1 ,
                (select OBJECT_NAME(f.rkeyid) as rtableName,col.name, f.constid as temp
                 from syscolumns col,sysforeignkeys f
                 where f.rkeyid=col.id
                 and f.rkey=col.colid
                 and f.constid in
                 ( select distinct(id)
                   from sysobjects
                   where OBJECT_NAME(parent_obj)='{0}'
                   and xtype='F'
                 )
                ) as t2
                where t1.temp=t2.temp
                ", tableName);
            return ExecuteDataTable(connectionString, commandText);
        }

        /// <summary>
        /// 取得唯一性字段列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> GetUniqueColumnList(string connectionString, string tableName)
        {
            DataSet dsConstraint = ExecuteDataSet(connectionString, string.Format("exec sp_helpconstraint '{0}'", tableName));
            List<string> lstUniqueColumn = new List<string>();
            foreach (DataRow drCurrent in dsConstraint.Tables[1].Rows)
            {
                if (drCurrent["constraint_type"].ToString().IndexOf("UNIQUE", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    lstUniqueColumn.Add(drCurrent["constraint_keys"].ToString());
                }
            }
            return lstUniqueColumn;
        }
        /// <summary>
        /// 取得字段表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetColumns(string connectionString, string tableName)
        {
            string commandText = string.Format("exec sp_columns '{0}'", tableName);
            return ExecuteDataTable(connectionString, commandText);
        }
        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static bool ExistsTable(string connectionString, string tableName)
        {
            string commandText = string.Format("select count(*) from sysobjects where xtype='u' and name='{0}'", tableName);
            int count = Convert.ToInt32(ExecuteScalar(connectionString, commandText));
            return count != 0;
        }
        /// <summary>
        /// 取得主键表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetPrimaryKeyTable(string connectionString, string tableName)
        {
            string commandText = string.Format("exec sp_pkeys '{0}'", tableName);
            DataTable dtPrimaryKey = ExecuteDataTable(connectionString, commandText);
            return dtPrimaryKey;
        }
        /// <summary>
        /// 取得主键列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> GetPrimaryKeyList(string connectionString, string tableName)
        {
            DataTable dtPrimaryKey = GetPrimaryKeyTable(connectionString, tableName);
            List<string> lstPrimaryKey = new List<string>();
            foreach (DataRow drCurrent in dtPrimaryKey.Rows)
            {
                lstPrimaryKey.Add(drCurrent["COLUMN_NAME"].ToString());
            }
            return lstPrimaryKey;
        }
    }
}
