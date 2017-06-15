using Apps.WebChat.Core;
using Apps.WebChat.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Apps.WebChat.Controllers
{
    public class WeChatController : Controller
    {
        public static readonly string WeChatToken = ConfigurationManager.AppSettings["Token"];
        public static readonly string EncodingAESKey = ConfigurationManager.AppSettings["EncodingAESKey"];
        public static readonly string AppId = ConfigurationManager.AppSettings["AppId"];
        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (string.IsNullOrEmpty(WeChatToken))
            {
                return Content("请先设置Token！");
            }
            if (WXHelper.CheckSignature(WeChatToken, signature, timestamp, nonce))
            {
                return Content(echostr);
            }
            else
            {
                return Content("Failed:" + signature + ", " + WXHelper.GetSignature(timestamp, nonce, WeChatToken) + "。如果你在浏览器中看到这个，说明此URL可以植入微信后台。");
            }
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(WeChatRequestModel model)
        {
            Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
            byte[] requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            string requestStr = Encoding.UTF8.GetString(requestByte);
            var XML = "";

            if (!string.IsNullOrEmpty(requestStr))
            {
                //封装请求类
                var requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(requestStr);
                var rootElement = requestDocXml.DocumentElement;

                if (rootElement == null)
                {
                    return Content("There is no element!");
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
                        XML = ResponseMessage.GetText(wxXmlModel.FromUserName, wxXmlModel.ToUserName, wxXmlModel.Content);
                        break;
                    case "Image"://图片
                        wxXmlModel.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        break;
                    //case "Voice"://语音
                    //    wxXmlModel.PicUrl = rootElement.SelectSingleNode("Media_id").InnerText;
                    //    break;
                    //case "Video"://视频
                    //    wxXmlModel.PicUrl = rootElement.SelectSingleNode("Media_id").InnerText;
                    //    break;
                    //case "Shortvideo"://小视频
                    //    wxXmlModel.PicUrl = rootElement.SelectSingleNode("Media_id").InnerText;
                    //    break;
                    //case "Location"://位置
                    //    wxXmlModel.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                    //    wxXmlModel.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                    //    break;

                    case "event"://事件

                        wxXmlModel.Event = rootElement.SelectSingleNode("Event").InnerText;
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
                        if (wxXmlModel.Event != "TEMPLATESENDJOBFINISH")//关注类型
                        {
                            wxXmlModel.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                        break;

                    default:
                        break;
                }

            }
            return Content(XML);
        }
    }
}