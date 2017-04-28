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
        [Dependency]
        public IFlow_StepBLL step_BLL { get; set; }
        [Dependency]
        public IFlow_TypeBLL t_BLL { get; set; }
        [Dependency]
        public IFlow_StepRuleBLL stepRule_BLL { get; set; }
        // GET: Flow/Flow_Form
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
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
            //html could not pass the  parameters,use the below code to set values.
            pager.order = "Desc";
            pager.page = 1;
            pager.sort = "Id";
            pager.rows = 1000;
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
            GridPager setPager = new GridPager()
            {
                rows = 1000,
                page = 1,
                sort = "Id",
                order = "Desc"
            };
            ViewBag.FlowType = new SelectList(t_BLL.GetList(ref setPager, "").Distinct(), "Id", "Name");
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

        [SupportFilter(ActionName = "Edit")]
        public ActionResult EditStep(string id)
        {
            ViewBag.Perm = GetPermission();
            Flow_FormModel flowFormModel = m_BLL.GetById(id);
            GridPager setNoPagerDescBySort = new GridPager
            {
                sort = "Id",
                rows = 100,
                order = "desc",
                page = 1
            };
            List<Flow_StepModel> stepList = step_BLL.GetList(ref setNoPagerDescBySort, flowFormModel.Id);

            foreach (var r in stepList)
            {
                r.stepRuleList = GetStepRuleListByStepId(r.Id);
            }

            flowFormModel.stepList = stepList;//获取表单关联的步骤
            ViewBag.Form = flowFormModel;
            Flow_StepModel model = new Flow_StepModel();
            model.FormId = flowFormModel.Id;
            model.IsEditAttr = true;
            return View(model);

        }

        private List<Flow_StepRuleModel> GetStepRuleListByStepId(string id)
        {

            GridPager pager = new GridPager()
            {
                rows = 100,
                order = "desc",
                sort = "Id",
                page = 1
            };
            List<Flow_StepRuleModel> stepList = stepRule_BLL.GetList(ref pager, id);
            return stepList;
            //throw new NotImplementedException();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult EditStep(Flow_StepModel model)
        {
            model.Id = ResultHelper.NewId;
            if (model != null && ModelState.IsValid)
            {
                if (step_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "Flow_Step");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed, model.Id));
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

        [SupportFilter(ActionName = "Edit")]
        public ActionResult StepList(string id)
        {
            ViewBag.FormId = id;
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult GetStepList(GridPager pager, string id)
        {
            List<Flow_StepModel> stepList = step_BLL.GetList(ref pager, id);
            int i = 1;
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in stepList
                        select new Flow_StepModel()
                        {
                            StepNo = "第 " + (i++) + " 步",
                            Id = r.Id,
                            Name = r.Name,
                            Remark = r.Remark,
                            Sort = r.Sort,
                            FormId = r.FormId,
                            FlowRule = r.FlowRule,
                            Action = "<a href='javascript:SetRule(\"" + r.Id + "\")'>分支(" + GetStepRuleListByStepId(r.Id).Count() + ")</a>"
                        }).ToArray()
            };
            return Json(json);
        }

        [SupportFilter(ActionName = "Edit")]
        public ActionResult StepRuleList(string stepId, string formId)
        {
            //获取现有的步骤
            GridPager pager = new GridPager()
            {
                rows = 100,
                order = "desc",
                sort = "Id",
                page = 1
            };
            Flow_FormModel flowFormModel = m_BLL.GetById(formId);
            List<Flow_FormAttrModel> attrList = new List<Flow_FormAttrModel>();//获取表单关联的字段
            attrList = GetAttrList(flowFormModel);
            List<Flow_StepModel> stepList = step_BLL.GetList(ref pager, formId);

            ViewBag.StepId = stepId;
            ViewBag.AttrList = attrList;
            ViewBag.StepList = stepList;
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult GetStepRuleList(string stepId)
        {
            GridPager pager = new GridPager()
            {
                rows = 100,
                order = "desc",
                sort = "Id",
                page = 1
            };
            List<Flow_StepRuleModel> stepList = stepRule_BLL.GetList(ref pager, stepId);
            int i = 1;
            var json = new
            {
                rows = (from r in stepList
                        select new Flow_StepRuleModel()
                        {
                            Mes = "分支" + (i++),
                            Id = r.Id,
                            StepId = r.StepId,
                            AttrId = r.AttrId,
                            AttrName = r.AttrName,
                            Operator = r.Operator,
                            Result = r.Result,
                            NextStep = r.NextStep,
                            NextStepName = r.NextStepName,
                            Action = "<a href='#' title='删除' class='icon-remove' onclick='DeleteEvent(\"" + r.Id + "\")'></a>"
                        }).ToArray()
            };
            return Json(json);
        }
        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult CreateStepEvent(Flow_StepRuleModel model)
        {
            model.Id = ResultHelper.NewId;
            if (model != null && ModelState.IsValid)
            {
                if (stepRule_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",stepId" + model.StepId, "成功", "创建", "Flow_StepRule");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed, model.Id));
                }
                else
                {
                    string errorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",StepId" + model.Id + "," + errorCol, "失败", "创建", "Flow_StepRule");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + errorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult DeleteStepRule(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (stepRule_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "Flow_StepRule");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "Flow_StepRule");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }

            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }
        }

        //获取已经添加的字段
        private List<Flow_FormAttrModel> GetAttrList(Flow_FormModel model)
        {
            List<Flow_FormAttrModel> list = new List<Flow_FormAttrModel>();
            Flow_FormAttrModel attrModel = new Flow_FormAttrModel();

            #region 处理字段
            //获取对象的类型，myclass
            Type formType = model.GetType();
            //查找名称为"MyProperty1"的属性
            string[] attrStr = {"AttrA", "AttrB", "AttrC", "AttrD", "AttrE", "AttrF", "AttrG", "AttrH", "AttrI", "AttrJ", "AttrK"
                                  , "AttrL", "AttrM", "AttrN", "AttrO", "AttrP", "AttrQ", "AttrR", "AttrS", "AttrT", "AttrU"
                                  , "AttrV", "AttrW", "AttrX", "AttrY", "AttrZ"};
            foreach (string str in attrStr)
            {
                object o = formType.GetProperty(str).GetValue(model, null);
                if (o != null)
                {
                    //查找model类的Class对象的"str"属性的值
                    attrModel = attr_BLL.GetById(o.ToString());
                    list.Add(attrModel);
                }
            }
            #endregion
            return list;
        }

        #endregion

        #region 详细
        //[SupportFilter]
        //public ActionResult Details(string id)
        //{
        //    ViewBag.Perm = GetPermission();
        //    Flow_FormModel entity = m_BLL.GetById(id);
        //    return View(entity);
        //}

        [SupportFilter(ActionName = "Edit")]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            Flow_FormModel flowFormModel = m_BLL.GetById(id);
            //获取现有的步骤
            GridPager gridPager = new GridPager()
            {
                rows = 1000,
                page = 1,
                sort = "Id",
                order = "asc"
            };
            flowFormModel.stepList = new List<Flow_StepModel>();
            flowFormModel.stepList = step_BLL.GetList(ref gridPager, flowFormModel.Id);
            for (int i = 0; i < flowFormModel.stepList.Count; i++)//获取步骤下面的步骤规则
            {
                flowFormModel.stepList[i].stepRuleList = new List<Flow_StepRuleModel>();
                flowFormModel.stepList[i].stepRuleList = GetStepRuleListByStepId(flowFormModel.stepList[i].Id);

            }
            return View(flowFormModel);
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