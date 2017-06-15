﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.WebChat.Models
{
    public class ResponseMessage
    {
        #region 接收的类型
        /// <summary>
        /// 接收文本
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string GetText(string FromUserName, string ToUserName, string Content)
        {
            CommonMethod.WriteTxt(Content);//接收的文本消息
            string XML = "";
            switch (Content)
            {
                case "ok":
                    XML = ReText(FromUserName, ToUserName, "测试确实ok！");
                    break;
                case "我擦":
                    XML = ReArticle(FromUserName, ToUserName, "我擦成功了", "点击查看", "http:xxx.com/xxx.png", "http://xxx.com");
                    break;
                default:
                    XML = ReText(FromUserName, ToUserName, "测试正常");
                    break;
            }
            return XML;
        }

        public static string GetImage(string FromUserName, string ToUserName, string MediaId)
        {
            string xml = ReImage(FromUserName, ToUserName, MediaId);
            return xml;

        }
        /// <summary>
        /// 未关注扫描带参数二维码
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="EventKey"></param>
        /// <returns></returns>
        public static string SubScanQrcode(string FromUserName, string ToUserName, string EventKey)
        {
            return "";
        }

        /// <summary>
        /// 已关注扫描带参数二维码
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="EventKey"></param>
        /// <returns></returns>
        public static string ScanQrcode(string FromUserName, string ToUserName, string EventKey)
        {
            return "";
        }
        #endregion

        #region 回复方式
        /// <summary>
        /// 回复文本
        /// </summary>
        /// <param name="FromUserName">发送给谁(openid)</param>
        /// <param name="ToUserName">来自谁(公众账号ID)</param>
        /// <param name="Content">回复类型文本</param>
        /// <returns>拼凑的XML</returns>
        public static string ReText(string FromUserName, string ToUserName, string Content)
        {
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[text]]></MsgType>";//回复类型文本
            XML += "<Content><![CDATA[" + Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";//回复内容 FuncFlag设置为1的时候，自动星标刚才接收到的消息，适合活动统计使用
            return XML;
        }

        /// <summary>
        /// 回复单图文
        /// </summary>
        /// <param name="FromUserName">发送给谁(openid)</param>
        /// <param name="ToUserName">来自谁(公众账号ID)</param>
        /// <param name="Title">标题</param>
        /// <param name="Description">详情</param>
        /// <param name="PicUrl">图片地址</param>
        /// <param name="Url">地址</param>
        /// <returns>拼凑的XML</returns>
        public static string ReArticle(string FromUserName, string ToUserName, string Title, string Description, string PicUrl, string Url)
        {
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>1</ArticleCount><Articles>";
            XML += "<item><Title><![CDATA[" + Title + "]]></Title><Description><![CDATA[" + Description + "]]></Description><PicUrl><![CDATA[" + PicUrl + "]]></PicUrl><Url><![CDATA[" + Url + "]]></Url></item>";
            XML += "</Articles><FuncFlag>0</FuncFlag></xml>";
            return XML;
        }

        /// <summary>
        /// 多图文回复
        /// </summary>
        /// <param name="FromUserName">发送给谁(openid)</param>
        /// <param name="ToUserName">来自谁(公众账号ID)</param>
        /// <param name="ArticleCount">图文数量</param>
        /// <param name="dtArticle"></param>
        /// <returns></returns>
        public static string ReArticle(string FromUserName, string ToUserName, int ArticleCount, System.Data.DataTable dtArticle)
        {
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + ArticleCount + "</ArticleCount><Articles>";
            foreach (System.Data.DataRow Item in dtArticle.Rows)
            {
                XML += "<item><Title><![CDATA[" + Item["Title"] + "]]></Title><Description><![CDATA[" + Item["Description"] + "]]></Description><PicUrl><![CDATA[" + Item["PicUrl"] + "]]></PicUrl><Url><![CDATA[" + Item["Url"] + "]]></Url></item>";
            }
            XML += "</Articles><FuncFlag>0</FuncFlag></xml>";
            return XML;
        }
        /// <summary>
        /// 图片回复
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="MediaId"></param>
        /// <returns></returns>
        public static string ReImage(string FromUserName, string ToUserName, string MediaId)
        {
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[image]]></MsgType><Image>";
            XML += "<MediaId><![CDATA[" + MediaId + "]]></MediaId>";
            XML += "</Image><FuncFlag>0</FuncFlag></xml>";
            return XML;
        }
        #endregion
    }
}