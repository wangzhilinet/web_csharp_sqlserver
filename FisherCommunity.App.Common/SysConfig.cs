using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FisherCommunity.App.Utility;
using System.Web;
using System.Windows.Forms;
using System.IO;

namespace FisherCommunity.App.Common
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public partial class SysConfig
    {
        private static string _connectionString = null;
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    //取得数据库连接串配置
                    _connectionString = ConfigurationHelper.GetSetting("ConnectionString");
                    //如果数据库连接串配置不存在，则使用组合配置
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        _connectionString = SqlServerHelper.GetConnectionString(SSPIEnabled, DatabaseServer, DatabaseName, DatabaseUser, DatabasePassword);
                    }
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        /// <summary>
        /// 数据库名
        /// </summary>
        public static string DatabaseName
        {
            get
            {
                return ConfigurationHelper.GetSetting("databaseName", "StroApp");
            }
        }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public static string DatabaseServer
        {
            get
            {
                return ConfigurationHelper.GetSetting("databaseServer", ".");
            }
        }
        /// <summary>
        /// 数据库用户
        /// </summary>
        public static string DatabaseUser
        {
            get
            {
                return ConfigurationHelper.GetSetting("databaseUser", "sa");
            }
        }
        /// <summary>
        /// 数据库密码
        /// </summary>
        public static string DatabasePassword
        {
            get
            {
                string databasePassword = Base64Encoder.Decode(ConfigurationHelper.GetSetting("databasePassword", "123456"));
                return databasePassword;
            }
        }
        /// <summary>
        /// 代理商初始密码
        /// </summary>
        public static string AgentPassword
        {
            get
            {
                string agentPassword = ConfigurationHelper.GetSetting("AgentPassword", "123456");
                return agentPassword;
            }
        }
        /// <summary>
        /// 是否使用SSPI
        /// </summary>
        public static bool SSPIEnabled
        {
            get
            {
                return ConfigurationHelper.GetBoolSetting("SSPIEnabled", true);
            }
        }
        /// <summary>
        /// 取得master库数据库连接串
        /// </summary>
        public static string MasterConnectionString
        {
            get
            {
                string connectionString = SqlServerHelper.GetConnectionString(SSPIEnabled, DatabaseServer, "master", DatabaseUser, DatabasePassword);
                return connectionString;
            }
        }
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string OLEConnectionString
        {
            get
            {
                return ConfigurationHelper.GetSetting("OLEConnectionString");
            }
        }
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public static string UploadDirectoryPath
        {
            get
            {
                return RootPath + "\\" + UploadDirectory;
            }
        }
        /// <summary>
        /// 站点根目录
        /// </summary>
        public static string RootPath
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return Application.StartupPath;
                }
                return HttpContext.Current.Server.MapPath("~");
            }
        }
        /// <summary>
        /// 上传文件目录名
        /// </summary>
        public static string UploadDirectory
        {
            get
            {
                string uploadfile = ConfigurationHelper.GetSetting("UploadDirectory", "UploadFile");
                return uploadfile;
            }
        }
        /// <summary>
        /// 下载文件目录名
        /// </summary>
        public static string DownloadDirecotry
        {
            get
            {
                string downloadDirecotry = ConfigurationHelper.GetSetting("DownloadDirecotry", "DownloadFile");
                return downloadDirecotry;
            }
        }
        /// <summary>
        /// 下载文件路径
        /// </summary>
        public static string DownloadDirectoryPath
        {
            get
            {
                return RootPath + "\\" + DownloadDirecotry;
            }
        }
        /// <summary>
        /// 重置表配置文件路径
        /// </summary>
        public static string DataResetTablesConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("DataResetTablesConfigFilePath", "Config/DataResetTables.xml");
                string resetTablesFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(resetTablesFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 备份信息文件路径
        /// </summary>
        public static string BackupInfoFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("BackupInfoFilePath", "Config/BackupInfo.xml");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 自动备份保留天数
        /// </summary>
        public static int AutoBackupRemainDays
        {
            get
            {
                return ConfigurationHelper.GetIntSetting("AutoBackupRemainDays", 7);
            }
        }
        /// <summary>
        /// 数据库备份路径
        /// </summary>
        public static string DatabaseBackupDirectory
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("DatabaseBackupDirectory", "Backup");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new DirectoryInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 导出配置文件路径
        /// </summary>
        public static string ExportConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("ExportConfigFilePath", "Config/ExportConfig.xml");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 导入配置文件路径
        /// </summary>
        public static string ImportConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("ImportConfigFilePath", "Config/ImportConfig.xml");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 导出配置文件路径
        /// </summary>
        public static string LoginHandlerConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("LoginHandlerConfigFilePath", "Config/LoginHandlers.xml");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 导入配置文件路径
        /// </summary>
        public static string ImageRootPath
        {
            get
            {
                string imageRootPath = RootPath + "\\" + SysConstants.ImageDirectoryName;
                return imageRootPath;
            }
        }
        /// <summary>
        /// 下载处理类配置文件路径
        /// </summary>
        public static string DownloadHandlerConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("DownloadHandlerConfigFilePath", "Config/DownloadHandlers.xml");
                string downloadHandlerFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(downloadHandlerFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 下载处理类配置文件路径
        /// </summary>
        public static string ApproveHandlerConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("ApproveHandlerConfigFilePath", "Config/ApproveHandlers.xml");
                string approveHandlerFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(approveHandlerFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 服务处理类配置文件路径
        /// </summary>
        public static string ServiceHandlerConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("ServiceHandlerConfigFilePath", "Config/ServiceHandlers.xml");
                string serviceHandlerFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(serviceHandlerFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 服务地址配置文件路径
        /// </summary>
        public static string ServiceUrlConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("ServiceUrlConfigFilePath", "Config/ServiceUrls.xml");
                string serviceUrlFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(serviceUrlFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 服务处理类配置文件路径
        /// </summary>
        public static string APICallHandlerConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("APICallHandlerConfigFilePath", "Config/APICallHandlers.xml");
                string serviceHandlerFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(serviceHandlerFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 小器件配置文件路径
        /// </summary>
        public static string WidgetConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("WidgetConfigFilePath", "Config/Widgets.xml");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 基础工资项配置文件路径
        /// </summary>
        public static string BasicSalaryAccountItemConfigFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("BasicSalaryAccountItemConfigFilePath", "Config/BasicSalaryAccountItems.xml");
                string approveHandlerFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(approveHandlerFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 参照根目录
        /// </summary>
        public static string SelectionRootDirectory
        {
            get
            {
                string relativeDirectoryPath = ConfigurationHelper.GetSetting("SelectionRootDirectory", "Config/Selection");
                string absoluteDirectoryPath = Path.Combine(RootPath, relativeDirectoryPath);
                string fullName = new FileInfo(absoluteDirectoryPath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 页面配置根目录
        /// </summary>
        public static string PageRootDirectory
        {
            get
            {
                string relativeDirectoryPath = ConfigurationHelper.GetSetting("PageRootDirectory", "Config/Page");
                string absoluteDirectoryPath = Path.Combine(RootPath, relativeDirectoryPath);
                string fullName = new FileInfo(absoluteDirectoryPath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 证书文件路径
        /// </summary>
        public static string LicenseFilePath
        {
            get
            {
                string relativeFilePath = ConfigurationHelper.GetSetting("LicenseFilePath", "Config/License.xml");
                string absoluteFilePath = Path.Combine(RootPath, relativeFilePath);
                string fullName = new FileInfo(absoluteFilePath).FullName;
                return fullName;
            }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public static string ProductCode
        {
            get
            {
                return "StroApp";
            }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public static string ProductName
        {
            get
            {
                return "思卓企业应用系统";
            }
        }
        /// <summary>
        /// 产品版本
        /// </summary>
        public static string ProductVersion
        {
            get
            {
                return "3.0";
            }
        }
        /// <summary>
        /// 注册地址
        /// </summary>
        public static string RegisterAddress
        {
            get
            {
                return ConfigurationHelper.GetSetting("RegisterAddress", "http://service.strosoft.com/Common/GetLicense.ashx");
            }
        }
        /// <summary>
        /// 检查试用地址
        /// </summary>
        public static string CheckTryingAddress
        {
            get
            {
                return ConfigurationHelper.GetSetting("CheckTryingAddress", "http://service.strosoft.com/Common/CheckTrying.ashx");
            }
        }
        /// <summary>
        /// 系统版本
        /// </summary>
        public static string SystemVersion
        {
            get
            {
                return "3.0.0.0";
            }
        }
        /// <summary>
        /// 取得配置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetSetting(string key, string defaultValue)
        {
            return ConfigurationHelper.GetSetting(key, defaultValue);
        }
        /// <summary>
        /// 取得配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            return GetSetting(key, null);
        }
        /// <summary>
        /// 管理端根地址
        /// </summary>
        public static string AdminRootUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AdminRootUrl", "http://app.strosoft.com");
            }
        }
        /// <summary>
        /// 前端根地址
        /// </summary>
        public static string FrontRootUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("FrontRootUrl", "http://app.strosoft.com");
            }
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        public static string LoginPage
        {
            get
            {
                return ConfigurationHelper.GetSetting("LoginPage", "~/Login.aspx");
            }

        }
        /// <summary>
        /// 主页面
        /// </summary>
        public static string MainPage
        {
            get
            {
                return ConfigurationHelper.GetSetting("MainPage", "Main.aspx");
            }

        }
        /// <summary>
        /// 不检查权限
        /// </summary>
        public static bool IgnorePermission
        {
            get
            {
                return ConfigurationHelper.GetBoolSetting("IgnorePermission", false);
            }
        }
        /// <summary>
        /// 桌面页面
        /// </summary>
        public static string DashboardPage
        {
            get
            {
                return ConfigurationHelper.GetSetting("DashboardPage", "~/Common/Dashboard.aspx");
            }

        }
    }
}
