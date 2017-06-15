using Apps.WebChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;

namespace Apps.WebChat.Core
{
    /// <summary>
    /// Summary description for WCApi
    /// </summary>
    public class WCApi : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.HttpMethod.ToLower() == "post")
            {
                //回复消息的时候也需要验证消息，这个很多开发者没有注意这个，存在安全隐患  
                //微信中 谁都可以获取信息 所以 关系不大 对于普通用户 但是对于某些涉及到验证信息的开发非常有必要
                if (CheckSignature())
                {
                    //接受信息
                    ReceiveXML();
                }
                else
                {
                    HttpContext.Current.Response.Write("消息并非来自微信");
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                CheckWeChat();
            }
            //context.Response.Write("Hello World");
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 接受消息

        private void ReceiveXML()
        {
            var requestStream = HttpContext.Current.Request.InputStream;
            var requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            var requestStr = Encoding.UTF8.GetString(requestByte);

            if (!string.IsNullOrEmpty(requestStr))
            {
                //封装请求类
                var requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(requestStr);
                var rootElement = requestDocXml.DocumentElement;

                if (rootElement == null)
                {
                    return;
                }

                var wxXmlModel = new WxXmlModel
                {
                    ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText,
                    FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText,
                    CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText,
                    MsgType = rootElement.SelectSingleNode("MsgType").InnerText
                };

                switch (wxXmlModel.MsgType)
                {
                    case "Text"://文本
                        wxXmlModel.Content = rootElement.SelectSingleNode("Content").InnerText;
                        break;
                    case "Image"://图片
                        wxXmlModel.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        break;
                    case "event"://事件
                        wxXmlModel.Event = rootElement.SelectSingleNode("Event").InnerText;
                        if (wxXmlModel.Event != "TEMPLATESENDJOBFINISH")//关注类型
                        {
                            wxXmlModel.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                        break;

                    default:
                        break;
                }
                ResponseXML(wxXmlModel);
            }
        }


        #endregion
        #region 回复消息
        private void ResponseXML(WxXmlModel wxXmlModel)
        {
            var QrCodeApi = new QrCodeApi();
            var XML = "";
            switch (wxXmlModel.MsgType)
            {
                case "text"://文本回复
                    XML = ResponseMessage.GetText(wxXmlModel.FromUserName, wxXmlModel.ToUserName, wxXmlModel.Content);
                    break;
                case "event":
                    switch (wxXmlModel.Event)
                    {
                        case "subscribe":
                            if (string.IsNullOrEmpty(wxXmlModel.EventKey))
                            {
                                XML = ResponseMessage.GetText(wxXmlModel.FromUserName, wxXmlModel.ToUserName, "关注成功");
                            }
                            else
                            {
                                XML = ResponseMessage.GetText(wxXmlModel.FromUserName, wxXmlModel.ToUserName, wxXmlModel.EventKey);
                            }
                            break;

                        case "SCAN":
                            XML = ResponseMessage.ScanQrcode(wxXmlModel.FromUserName, wxXmlModel.ToUserName, wxXmlModel.EventKey);//扫描已关注二维码已关注，直接推送事件
                            break;
                    }
                    break;
                default://默认回复
                    break;
            }
            HttpContext.Current.Response.Write(XML);
            HttpContext.Current.Response.End();
        }
        #endregion
        /// <summary>
        /// 返回随机数表示验证成功
        /// </summary>
        private void CheckWeChat()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["echoStr"]))
            {
                HttpContext.Current.Response.Write("消息并非来自微信");
                HttpContext.Current.Response.End();
            }
            var echoStr = HttpContext.Current.Request.QueryString["echoStr"];
            if (CheckSignature())
            {
                HttpContext.Current.Response.Write(echoStr);
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <returns></returns>
        public bool CheckSignature()
        {
            string token = "JasonWebChatToken";
            //获取参数
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
            string echostr = HttpContext.Current.Request.QueryString["echostr"];

            //将token、timestamp、nonce三个参数进行字典序排序
            string[] array = { token, timestamp, nonce };
            Array.Sort(array);

            //将三个参数字符串拼接成一个字符串进行sha1加密
            string str = string.Join("", array);
            str = Sha1(str);

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

        /// <summary>
        /// 基于Sha1的自定义加密字符串方法：输入一个字符串，返回一个由40个字符组成的十六进制的哈希散列（字符串）。
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
        public string Sha1(string str)
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