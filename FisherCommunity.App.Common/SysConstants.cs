using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FisherCommunity.App.Common
{
    /// <summary>
    /// 应用程序常量
    /// </summary>
    public partial class SysConstants
    {
        /// <summary>
        /// 数字格式化字符串
        /// </summary>
        public const string NumberFormat = "#0.00";
        /// <summary>
        /// 空值文本
        /// </summary>
        public const string NullText = " ";
        /// <summary>
        /// 全部文本
        /// </summary>
        public const string AllText = "全部";
        /// <summary>
        /// 超级管理员用户名
        /// </summary>
        public const string AdminUserName = "admin";
        /// <summary>
        /// 超级管理员密码
        /// </summary>
        public const string AdminPassword = "admin";
        /// <summary>
        /// 超级管理员登录名
        /// </summary>
        public const string AdminLoginName = "admin";
        /// <summary>
        /// 超级管理员角色名
        /// </summary>
        public const string AdminRoleName = "超级管理员";
        /// <summary>
        /// 图片目录名称
        /// </summary>
        public const string ImageDirectoryName = "Images";
        /// <summary>
        /// 校验图片文本
        /// </summary>
        public const string ValidImageText = "ValidImageText";  
        /// <summary>
        /// 默认最大用户数
        /// </summary>
        public const int DefaultMaxUserCount = 3;
        /// <summary>
        /// 默认最大活动用户数
        /// </summary>
        public const int DefaultMaxActiveUserCount = 3;
        /// <summary>
        /// 加密狗PID
        /// </summary>
        public const string PID = "B264AD72";
        /// <summary>
        /// 默认明细页面宽度
        /// </summary>
        public const int DefaultDetailPageWidth = 800;
        /// <summary>
        /// 默认明细页面高度
        /// </summary>
        public const int DefaultDetailPageHeight = 500;
        /// <summary>
        /// 默认明细页面高度
        /// </summary>
        public const int DefaultFormItemCaptoinWidth = 100;
        /// <summary>
        /// 短信验证码Session名称
        /// </summary>
        public const string SmsCodeSessionName = "SmsCode";
    }
}
