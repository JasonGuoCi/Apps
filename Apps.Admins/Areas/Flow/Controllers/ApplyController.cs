using Apps.Admins.Core;
using Apps.Common;
using Apps.Flow.IBLL;
using Apps.IBLL;
using Apps.Models.Flow;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Areas.Flow.Controllers
{
    public class ApplyController : BaseController
    {
        [Dependency]
        public ISysUserBLL userBLL { get; set; }
        [Dependency]
        public IFlow_TypeBLL flowType_BLL { get; set; }
        [Dependency]
        public IFlow_FormBLL formBLL { get; set; }
        [Dependency]
        public IFlow_FormAttrBLL formAttrBLL { get; set; }
        [Dependency]
        public IFlow_FormContentBLL formContentBLL { get; set; }
        [Dependency]
        public IFlow_StepBLL stepBLL { get; set; }
        [Dependency]
        public IFlow_StepRuleBLL stepRuleBLL { get; set; }
        [Dependency]
        public IFlow_FormContentStepCheckBLL stepCheckBLL { get; set; }
        [Dependency]
        public IFlow_FormContentStepCheckStateBLL stepCheckStateBLL { get; set; }

        ValidationErrors errors = new ValidationErrors();
        // GET: Flow/Apply
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult GetListByUserId(GridPager pager, string queryStr)
        {
            List<Flow_FormContentModel> list = formContentBLL.GetListByUserId(ref pager, queryStr, GetUserId());

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new Flow_FormContentModel()
                        {
                            Id = r.Id,
                            Title = r.Title,
                            UserId = r.UserId,
                            FormId = r.FormId,
                            FormLevel = r.FormLevel,
                            CreateTime = r.CreateTime,
                            TimeOut = r.TimeOut,
                            CurrentStep = r.CurrentStep,
                            CurrentState = r.CurrentState,
                            Action = "<a href='#' title='管理' onclick='ManageFlow(\"" + r.FormId + "\",\"" + r.Id + "\")'>管理</a>|<a href='#' title='图例' onclick='LookFlow(\"" + r.FormId + "\")'>图例</a>"

                        }).ToArray()
            };
            return Json(json);
        }

        public string GetCurrentStep(Flow_FormContentModel model)
        {
            string str = "结束";
            List<Flow_FormContentStepCheckModel> stepCheckModelList = stepCheckBLL.GetListByFormId(model.FormId, model.Id);
            for (int i = stepCheckModelList.Count - 1; i >= 0; i--)
            {
                if (stepCheckModelList[i].State == 2)
                {
                    str = stepBLL.GetById(stepCheckModelList[i].StepId).Name;
                }
            }
            return str;
        }

        public string GetCurrentState(Flow_FormContentModel model)
        {
            if (model.TimeOut < ResultHelper.NowTime)
            {
                return "<span style='color:#0094ff'>过期</span>";
            }

            List<Flow_FormContentStepCheckModel> stepCheckModelList = stepCheckBLL.GetListByFormId(model.FormId, model.Id);

            var v = from r in stepCheckModelList where r.State == 1 select r;
            if (v.Count() == stepCheckModelList.Count)
            {
                return "<span style='color:#6fce2f'>通过</span>";
            }

            var vv = from r in stepCheckModelList where r.State == 0 select r;
            if (vv.Count() > 0)
            {
                return "<span style='color:#ff6a00'>驳回</span>";
            }

            return "<span style='color:#ff6600'>待审核</span>";
        }

        #region 详细

        [SupportFilter(ActionName = "Details")]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            Flow_FormModel flowFormModel = formBLL.GetById(id);

            //获取现有的步骤
            GridPager pager = new GridPager()
            {
                rows = 1000,
                page = 1,
                sort = "Id",
                order = "asc"
            };

            flowFormModel.stepList = new List<Flow_StepModel>();
            flowFormModel.stepList = stepBLL.GetList(ref pager, flowFormModel.Id);
            for (int i = 0; i < flowFormModel.stepList.Count; i++)//获取步骤下面的步骤规则
            {
                flowFormModel.stepList[i].stepRuleList = new List<Flow_StepRuleModel>();
                flowFormModel.stepList[i].stepRuleList = GetStepRuleListByStepId(flowFormModel.stepList[i].Id);
            }

            return View(flowFormModel);
        }

        //获取步骤下面的规则
        private List<Flow_StepRuleModel> GetStepRuleListByStepId(string stepId)
        {
            List<Flow_StepRuleModel> list = stepRuleBLL.GetList(stepId);
            return list;
        }
        #endregion

        [SupportFilter(ActionName = "Details")]
        public ActionResult Edit(string formId, string id)
        {
            ViewBag.Perm = GetPermission();
            ViewBag.Html = ExceHtmlJs(formId);
            ViewBag.StepCheckMes = GetCurrentStepCheckMes(formId, id);
            Flow_FormContentModel model = formContentBLL.GetById(id);
            return View(model);
        }


        //根据设定公文，生成表单及控制条件
        private string ExceHtmlJs(string id)
        {
            //定义一个sb为生成HTML表单
            StringBuilder sbHtml = new StringBuilder();
            StringBuilder sbJS = new StringBuilder();

            sbJS.Append("<script type='text/javascript'>function CheckForm(){");
            Flow_FormModel model = formBLL.GetById(id);

            #region 判断流程是否有字段,有就生成HTML表单
            Type formType = model.GetType();
            //查找名称为"A-Z"的属性
            string[] arrStr = { "AttrA", "AttrB", "AttrC", "AttrD", "AttrE", "AttrF", "AttrG", "AttrH", "AttrI", "AttrJ", "AttrK"
                                  , "AttrL", "AttrM", "AttrN", "AttrO", "AttrP", "AttrQ", "AttrR", "AttrS", "AttrT", "AttrU"
                                  , "AttrV", "AttrW", "AttrX", "AttrY", "AttrZ"};
            foreach (var str in arrStr)
            {
                object o = formType.GetProperty(str).GetValue(model, null);

                if (o != null)
                {
                    //查找model类的Class对象的"str"属性的值
                    sbHtml.Append(GetHtml(o.ToString(), str.Replace("Attr", ""), ref sbJS));

                }
            }
            #endregion

            sbJS.Append("return true;}</script>");
            ViewBag.HtmlJS = sbJS.ToString();
            return sbHtml.ToString();
        }

        //对比
        private bool JudgeVal(string attrId, string rVal, string cVal, string lVal)
        {
            string attrType = formAttrBLL.GetById(attrId).AttrType;
            return new FlowHelper().Judge(attrType, rVal, cVal, lVal);
        }
        private string GetHtml(string id, string no, ref StringBuilder sbJS)
        {
            StringBuilder sb = new StringBuilder();
            Flow_FormAttrModel attrModel = formAttrBLL.GetById(id);
            sb.AppendFormat("<tr><td style='width:100px; text-align:right;'>{0}:</td>", attrModel.Title);
            //获取指定类型的HTML表单
            sb.AppendFormat("<td>{0}</td></tr>", new FlowHelper().GetInput(attrModel.AttrType, attrModel.Name, no));
            sbJS.Append(attrModel.CheckJS);
            return sb.ToString();

        }

        //获取环节所有信息
        public string GetCurrentStepCheckMes(string formId, string contentId)
        {
            string stepCheckMes = "";
            string stepCheckId = GetCurrentStepCheckId(formId, contentId);

            List<Flow_FormContentStepCheckModel> stepCheckModelList = stepCheckBLL.GetListByFormId(formId, contentId);
            for (int i = 0; i < stepCheckModelList.Count; i++)
            {
                Flow_FormContentStepCheckStateModel stepCheckStateModel = stepCheckStateBLL.GetByStepCheckId(stepCheckModelList[i].Id);
                stepCheckMes = stepCheckMes + "第" + (i + 1) + "步：审核人：" + stepCheckStateModel.UserId + " 审核意见：" + stepCheckStateModel.Reamrk + " 审核意见：" + (stepCheckStateModel.CheckFlag == 1 ? "通过" : stepCheckStateModel.CheckFlag == 0 ? "不通过" : "审核中") + "</br>";

            }

            return stepCheckMes;
        }

        public string GetCurrentStepCheckId(string formId, string contentId)
        {
            List<Flow_FormContentStepCheckModel> stepCheckModelList = stepCheckBLL.GetListByFormId(formId, contentId);
            return new FlowHelper().GetCurrentStepCheckIdByStepCheckModelList(stepCheckModelList);
        }
    }
}