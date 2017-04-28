using Apps.Admins.Core;
using Apps.Common;
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
    public class Flow_StepController : BaseController
    {
        [Dependency]
        public IFlow_StepBLL m_BLL { get; set; }
        [Dependency]
        public IFlow_FormBLL f_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        // GET: Flow/Flow_Step
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<Flow_StepModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new Flow_StepModel()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Remark = r.Remark,
                            Sort = r.Sort,
                            FormId = r.FormId,
                            FlowRule = r.FlowRule,
                            IsCustom = r.IsCustom,
                            IsAllCheck = r.IsAllCheck,
                            Execution = r.Execution,
                            CompulsoryOver = r.CompulsoryOver,
                            IsEditAttr = r.IsEditAttr

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
        public JsonResult Create(Flow_StepModel model)
        {
            model.Id = ResultHelper.NewId;
            //model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "Flow_Step");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "Flow_Step");
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
            Flow_StepModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Flow_StepModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "Flow_Step");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "Flow_Step");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }

        //[SupportFilter(ActionName = "Edit")]
        //public ActionResult EditStep(string id)
        //{
        //    ViewBag.Perm = GetPermission();
        //    Flow_FormModel flowFormModel = f_BLL.GetById(id);
        //    GridPager setNoPagerDescBySort = new GridPager
        //    {
        //        sort = "Id",
        //        rows = 100,
        //        order = "desc",
        //        page = 1
        //    };
        //    List<Flow_StepModel> stepList = m_BLL.GetList(ref setNoPagerDescBySort, flowFormModel.Id);

        //    foreach (var r in stepList)
        //    {
        //        r.stepRuleList = GetStepRuleListByStepId(r.Id);
        //    }

        //    flowFormModel.stepList = stepList;//获取表单关联的步骤
        //    ViewBag.Form = flowFormModel;
        //    Flow_StepModel model = new Flow_StepModel();
        //    model.FormId = flowFormModel.Id;
        //    model.IsEditAttr = true;
        //    return View(model);

        //}

        //private List<Flow_FormModel> GetStepRuleListByStepId(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //[SupportFilter(ActionName = "Edit")]
        //public JsonResult EditStep(Flow_StepModel model)
        //{
        //    model.Id = ResultHelper.NewId;
        //    if (model != null && ModelState.IsValid)
        //    {
        //        if (m_BLL.Create(ref errors, model))
        //        {
        //            LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "Flow_Step");
        //            return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed, model.Id));
        //        }
        //        else
        //        {
        //            string ErrorCol = errors.Error;
        //            LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "Flow_Step");
        //            return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
        //        }
        //    }
        //    else
        //    {
        //        return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
        //    }
        //}
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            Flow_StepModel entity = m_BLL.GetById(id);
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "Flow_Step");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "Flow_Step");
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