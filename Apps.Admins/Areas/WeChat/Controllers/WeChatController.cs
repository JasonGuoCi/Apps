using Apps.Admins.Core;
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

namespace Apps.Admins.Areas.WeChat.Controllers
{
    public class WeChatController : BaseController
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
            ViewBag.Perm = GetPermission();
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

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<WC_OfficalAccountsModel> list = account_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new WC_OfficalAccountsModel()
                        {

                            Id = r.Id,
                            OfficalId = r.OfficalId,
                            OfficalName = r.OfficalName,
                            OfficalCode = r.OfficalCode,
                            OfficalPhoto = r.OfficalPhoto,
                            OfficalKey = r.OfficalKey,
                            ApiUrl = r.ApiUrl,
                            Token = r.Token,
                            AppId = r.AppId,
                            AppSecret = r.AppSecret,
                            AccessToken = r.AccessToken,
                            Remark = r.Remark,
                            Enable = r.Enable,
                            IsDefault = r.IsDefault,
                            Category = r.Category,
                            CreateTime = r.CreateTime,
                            CreateBy = r.CreateBy,
                            ModifyTime = r.ModifyTime,
                            ModifyBy = r.ModifyBy

                        }).ToArray()

            };

            return Json(json);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(WC_OfficalAccountsModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (account_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalId" + model.OfficalName, "成功", "创建", "WC_OfficalAccounts");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalId" + model.OfficalName + "," + ErrorCol, "失败", "创建", "WC_OfficalAccounts");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            WC_OfficalAccountsModel entity = account_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(WC_OfficalAccountsModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (account_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalId" + model.OfficalName, "成功", "修改", "WC_OfficalAccounts");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalId" + model.OfficalName + "," + ErrorCol, "失败", "修改", "WC_OfficalAccounts");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            WC_OfficalAccountsModel entity = account_BLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (account_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "WC_OfficalAccounts");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "WC_OfficalAccounts");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }


        }
        #endregion
    }
}