using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FisherCommunity.App.Utility
{
    /// <summary>
    /// Base64编码类
    /// </summary>
    public class Base64Encoder
    {
        /// <summary>
        /// 转换成base64编码
        /// </summary>
        /// <returns></returns>
        public static string Encode(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return null;
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }
        /// <summary>
        /// 从base64编码转换
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decode(string base64Text)
        {
            if (string.IsNullOrEmpty(base64Text))
            {
                return null;
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64Text));
        }
    }
}
