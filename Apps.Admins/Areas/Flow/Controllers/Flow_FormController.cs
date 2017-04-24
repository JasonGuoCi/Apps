using Apps.Admins.Core;
using Apps.Common;
using Apps.Flow.BLL;
using Apps.Flow.IBLL;
using Apps.Models.Flow;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Areas.Flow.Controllers
{
    public class Flow_FormController : BaseController
    {
        [Dependency]
        public IFlow_FormBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [Dependency]
        public IFlow_FormAttrBLL attr_BLL { get; set; }
        // GET: Flow/Flow_Form
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<Flow_FormModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new Flow_FormModel()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Remark = r.Remark,
                            UsingDep = r.UsingDep,
                            TypeId = r.TypeId,
                            State = r.State,
                            CreateTime = r.CreateTime,
                            HtmlForm = r.HtmlForm,
                            AttrA = r.AttrA,
                            AttrB = r.AttrB,
                            AttrC = r.AttrC,
                            AttrD = r.AttrD,
                            AttrE = r.AttrE,
                            AttrF = r.AttrF,
                            AttrG = r.AttrG,
                            AttrH = r.AttrH,
                            AttrI = r.AttrI,
                            AttrJ = r.AttrJ,
                            AttrK = r.AttrK,
                            AttrL = r.AttrL,
                            AttrM = r.AttrM,
                            AttrN = r.AttrN,
                            AttrO = r.AttrO,
                            AttrP = r.AttrP,
                            AttrQ = r.AttrQ,
                            AttrR = r.AttrR,
                            AttrS = r.AttrS,
                            AttrT = r.AttrT,
                            AttrU = r.AttrU,
                            AttrV = r.AttrV,
                            AttrW = r.AttrW,
                            AttrX = r.AttrX,
                            AttrY = r.AttrY,
                            AttrZ = r.AttrZ

                        }).ToArray()

            };

            return Json(json);
        }
        [HttpPost]
        public JsonResult GetFormAttrList(GridPager pager, string queryStr)
        {
            List<Flow_FormAttrModel> list = attr_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new Flow_FormAttrModel()
                        {
                            Id = r.Id,
                            Title = r.Title,
                            Name = r.Name,
                            AttrType = r.AttrType,
                            CheckJS = r.CheckJS,
                            TypeId = r.TypeId,
                            CreateTime = r.CreateTime

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
        public JsonResult Create(Flow_FormModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "Flow_Form");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "Flow_Form");
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
            Flow_FormModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Flow_FormModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "Flow_Form");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "Flow_Form");
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
            Flow_FormModel entity = m_BLL.GetById(id);
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "Flow_Form");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "Flow_Form");
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