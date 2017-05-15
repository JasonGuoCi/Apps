using Apps.Common;
using Apps.Common.Handlers;
using Apps.IBLL;
using Apps.Models.WeChat;
using Microsoft.Practices.Unity;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Apps.WebChat.Controllers
{
    public class WeChatController : Controller
    {
        [Dependency]
        public IWC_OfficalAccountsBLL account_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        public static readonly string Token = "JasonWebChatToken";//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "9lR462ktbYvl95nPO8adXeSRA8r62ieVo7cEZxeESL0";//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = "wx56e163a2c608471b";//与微信公众账号后台的AppId设置保持一致，区分大小写。


        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
        {

            return Task.Factory.StartNew(() =>
            {
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    return echostr;//返回随机字符串则表示验证通过
                }
                else
                {
                    return "failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                        "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                }
            }).ContinueWith<ActionResult>(task => Content(task.Result));
        }

        [HttpPost]
        [ActionName("Index")]
        public Task<ActionResult> Post(PostModel postModel)
        {
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                WC_OfficalAccountsModel model = account_BLL.GetCurrentAccount();
                //没有参数
                if (string.IsNullOrEmpty(Request["id"]))
                {
                    return new WeixinResult("非法路径请求");
                }
                if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, model.Token))
                {
                    return new WeixinResult("参数错误！");
                }

                postModel.Token = Token;
                postModel.EncodingAESKey = EncodingAESKey; //根据自己后台的设置保持一致
                postModel.AppId = model.AppId;//根据自己后台的设置保持一致

                var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, 10);
                messageHandler.Execute(); //执行微信处理过程

                return new FixWeixinBugWeixinResult(messageHandler);

            }).ContinueWith<ActionResult>(task => task.Result);
        }

        public JsonResult GetToken()
        {
            GridPager setNoPagerAscById = new GridPager
            {
                sort = "Id",
                rows = 100,
                order = "asc",
                page = 1
            };
            List<WC_OfficalAccountsModel> list = account_BLL.GetList(ref setNoPagerAscById, "");
            foreach (var model in list)
            {
                if (!string.IsNullOrEmpty(model.Id) && !string.IsNullOrEmpty(model.AppSecret))
                {
                    model.AccessToken = Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(model.AppId, model.AppSecret).access_token;
                    model.ModifyTime = ResultHelper.NowTime;
                    account_BLL.Edit(ref errors, model);
                }
            }

            return Json(JsonHandler.CreateMessage(1, "成批更新成功"));
        }
    }
}