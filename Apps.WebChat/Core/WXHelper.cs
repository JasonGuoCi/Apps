using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Apps.WebChat.Core
{
    public class WXHelper
    {

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <returns></returns>
        public static bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            //string token = "JasonWebChatToken";
            //获取参数
            //string signature = HttpContext.Current.Request.QueryString["signature"];
            //string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            //string nonce = HttpContext.Current.Request.QueryString["nonce"];
            //string echostr = HttpContext.Current.Request.QueryString["echostr"];

            ////将token、timestamp、nonce三个参数进行字典序排序
            //string[] array = { token, timestamp, nonce };
            //Array.Sort(array);

            ////将三个参数字符串拼接成一个字符串进行sha1加密
            //string str = string.Join("", array);
            //str = Sha1(str);

            string str = GetSignature(timestamp, nonce, token);
            //开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
            if (string.IsNullOrEmpty(str) && str == signature)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static string GetSignature(string timestamp, string nonce, string token)
        {
            //将token、timestamp、nonce三个参数进行字典序排序
            string[] array = { token, timestamp, nonce };
            Array.Sort(array);

            //将三个参数字符串拼接成一个字符串进行sha1加密
            string str = string.Join("", array);
            str = Sha1(str);
            return str;
        }
        /// <summary>
        /// 基于Sha1的自定义加密字符串方法：输入一个字符串，返回一个由40个字符组成的十六进制的哈希散列（字符串）。
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
        public static string Sha1(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}