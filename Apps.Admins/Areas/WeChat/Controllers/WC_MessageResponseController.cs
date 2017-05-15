using Apps.Admins.Core;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.WeChat;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Areas.WeChat.Controllers
{
    public class WC_MessageResponseController : BaseController
    {
        [Dependency]
        public IWC_MessageResponseBLL m_BLL { get; set; }
        [Dependency]
        public IWC_OfficalAccountsBLL account_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        // GET: WeChat/WC_MessageResponse
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<WC_MessageResponseModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new WC_MessageResponseModel()
                        {

                            Id = r.Id,
                            OfficalAccountId = r.OfficalAccountId,
                            MessageRule = r.MessageRule,
                            Category = r.Category,
                            MatchKey = r.MatchKey,
                            TextContent = r.TextContent,
                            ImgTextContext = r.ImgTextContext,
                            ImgTextUrl = r.ImgTextUrl,
                            ImgTextLink = r.ImgTextLink,
                            MeidaUrl = r.MeidaUrl,
                            MeidaLink = r.MeidaLink,
                            Enable = r.Enable,
                            IsDefault = r.IsDefault,
                            Remark = r.Remark,
                            Sort = r.Sort,
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
        public JsonResult Create(WC_MessageResponseModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalAccountId" + model.OfficalAccountId, "成功", "创建", "WC_MessageResponse");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalAccountId" + model.OfficalAccountId + "," + ErrorCol, "失败", "创建", "WC_MessageResponse");
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
            WC_MessageResponseModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(WC_MessageResponseModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalAccountId" + model.OfficalAccountId, "成功", "修改", "WC_MessageResponse");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",OfficalAccountId" + model.OfficalAccountId + "," + ErrorCol, "失败", "修改", "WC_MessageResponse");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult PostData(WC_MessageResponseModel model)
        {
            WC_OfficalAccountsModel accountModel = account_BLL.GetCurrentAccount();
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = ResultHelper.NewId;
            }

            model.CreateBy = GetUserId();
            model.CreateTime = ResultHelper.NowTime;
            model.ModifyBy = GetUserId();
            model.ModifyTime = ResultHelper.NowTime;
            model.OfficalAccountId = accountModel.Id;
            model.Enable = true;
            model.IsDefault = true;

            if (m_BLL.PostData(ref errors, model))
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ", OfficalAccountId" + model.OfficalAccountId, "成功", "保存", "WC_MessageResponse");
                return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ", OfficalAccountId" + model.OfficalAccountId, "失败", "保存", "WC_MessageResponse");
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            WC_MessageResponseModel entity = m_BLL.GetById(id);
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
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "WC_MessageResponse");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "WC_MessageResponse");
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