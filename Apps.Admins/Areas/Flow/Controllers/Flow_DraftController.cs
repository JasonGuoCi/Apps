using Apps.Admins.Core;
using Apps.Common;
using Apps.Flow.IBLL;
using Apps.IBLL;
using Apps.Models.Flow;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Areas.Flow.Controllers
{
    public class Flow_DraftController : BaseController
    {
        [Dependency]
        public ISysUserBLL userBLL { get; set; }
        [Dependency]
        public IFlow_TypeBLL m_BLL { get; set; }

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
        public IFlow_FormContentStepCheckStateBLL stepCheckStateBLL { get; set; }
        [Dependency]
        public IFlow_FormContentStepCheckBLL formContentStepCheckBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        // GET: Flow/Flow_Draft
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            //GridPager setNoPagerAscBySort = new GridPager()
            //{
            //    rows = 100,
            //    order = "asc",
            //    sort = "Id",
            //    page = 1
            //};
            List<Flow_TypeModel> list = m_BLL.GetList(ref setNoPagerAscBySort, "");
            foreach (var item in list)
            {
                item.formList = new List<Flow_FormModel>();
                List<Flow_FormModel> formList = formBLL.GetListByTypeId(item.Id);
                item.formList = formList;
            }
            ViewBag.DraftList = list;
            return View();
        }

        [SupportFilter]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();
            ViewBag.Html = ExceHtmlJS(id);
            Flow_FormContentModel model = new Flow_FormContentModel();
            model.FormId = id;
            return View(model);
        }
        //根据设定公文，生成表单及控制条件
        private string ExceHtmlJS(string id)
        {
            //定义一个sb为生成HTML表单
            StringBuilder sbHtml = new StringBuilder();
            StringBuilder sbJS = new StringBuilder();
            sbJS.Append("<script type='text/javascript'>function CheckForm(){");
            Flow_FormModel model = formBLL.GetById(id);

            #region 判断流程是否有字段，有就生成HTML表单
            //获得对象的类型，model  利用反射原理
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
                    sbHtml.Append(JuageExc(o.ToString(), str, ref sbJS));
                }
            }
            #endregion

            sbJS.Append("return type}</script>");
            ViewBag.HtmlJS = sbJS.ToString();
            return sbHtml.ToString();
        }

        private string JuageExc(string attr, string no, ref StringBuilder sbJS)
        {
            if (!string.IsNullOrEmpty(attr))
            {
                return GetHtml(attr, no, ref sbJS);
            }
            return "";
        }
        //获取指定名称的HTML表单
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

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Flow_FormContentModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            model.UserId = GetUserId();

            if (model != null && ModelState.IsValid)
            {
                if (formContentBLL.Create(ref errors, model))
                {
                    //当前的Form模版
                    Flow_FormModel formModel = formBLL.GetById(model.FormId);

                    //创建成功后把步骤取出
                    List<Flow_StepModel> stepModelList = stepBLL.GetList(ref setNoPagerAscBySort, model.FormId);

                    //查询步骤
                    bool IsEnd = false;
                    foreach (Flow_StepModel stepModel in stepModelList)
                    {
                        List<Flow_StepRuleModel> stepRuleModelList = stepRuleBLL.GetList(stepModel.Id);
                        //获取规则判断流转方向
                        foreach (Flow_StepRuleModel stepRuleModel in stepRuleModelList)
                        {
                            string val = new FlowHelper().GetFormAttrVal(stepRuleModel.AttrId, formModel, model);

                            //有满足不流程结束的条件
                            if (!JudgeVal(stepRuleModel.AttrId, val, stepRuleModel.Operator, stepRuleModel.Result))
                            {
                                if (stepRuleModel.NextStep != "0")
                                {
                                    IsEnd = false;
                                }
                            }
                        }

                        //插入步骤审核表
                        Flow_FormContentStepCheckModel stepCheckModel = new Flow_FormContentStepCheckModel();
                        stepCheckModel.Id = ResultHelper.NewId;
                        stepCheckModel.ContentId = model.Id;
                        stepCheckModel.StepId = stepModel.Id;
                        stepCheckModel.State = 2;//0不通过1通过2审核中
                        stepCheckModel.StateFlag = false;//true此步骤审核完成
                        stepCheckModel.CreateTime = ResultHelper.NowTime;
                        stepCheckModel.IsEnd = IsEnd;//是否流程的最后一步

                        if (formContentStepCheckBLL.Create(ref errors, stepCheckModel))//新建步骤成功
                        {
                            //获得流转规则下的审核人员
                            List<string> userIdList = GetStepCheckMemberList(stepModel.Id, model.Id);
                            foreach (string userId in userIdList)
                            {
                                //批量建立步骤审核人表
                                Flow_FormContentStepCheckStateModel stepCheckStateModel = new Flow_FormContentStepCheckStateModel();
                                stepCheckStateModel.Id = ResultHelper.NewId;
                                stepCheckStateModel.StepCheckId = stepCheckModel.Id;
                                stepCheckStateModel.UserId = userId;
                                stepCheckStateModel.CheckFlag = 2;
                                stepCheckStateModel.Reamrk = "";
                                stepCheckStateModel.TheSeal = "";
                                stepCheckStateModel.CreateTime = ResultHelper.NowTime;
                                stepCheckStateBLL.Create(ref errors, stepCheckStateModel);
                            }
                        }

                        if (IsEnd)//如果是最后一步就无需要下面继续了
                        {
                            break;
                        }
                        IsEnd = true;
                    }

                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",AttrA" + model.AttrA, "成功", "创建", "Flow_FormContent");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",AttrA" + model.AttrA + "," + ErrorCol, "失败", "创建", "Flow_FormContent");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }

        public List<string> GetStepCheckMemberList(string stepId, string formContentId)
        {
            List<string> userModelList = new List<string>();
            Flow_StepModel stepModel = stepBLL.GetById(stepId);

            if (stepModel.FlowRule == "上级")
            {
                SysUserModel userModel = userBLL.GetById(GetUserId());
                string[] array = userModel.Lead.Split(',');//获得领导，可能有多个领导
                foreach (var item in array)
                {
                    userModelList.Add(item);
                }

            }
            else if (stepModel.FlowRule == "职位")
            {
                string[] array = stepModel.Execution.Split(',');//获得领导，可能有多个领导
                foreach (var item in array)
                {
                    List<SysUserModel> userList = userBLL.GetListByPostId(item);
                    foreach (SysUserModel userModel in userList)
                    {
                        userModelList.Add(userModel.Id);
                    }
                }
            }
            else if (stepModel.FlowRule == "部门")
            {
                GridPager pager = new GridPager()
                {
                    rows = 10000,
                    page = 1,
                    sort = "Id",
                    order = "desc"
                };
                string[] array = stepModel.Execution.Split(',');//获得领导，可能有多个领导
                foreach (string str in array)
                {
                    List<SysUserModel> userList = userBLL.GetUserByDepId(ref pager, str, "");
                    foreach (SysUserModel user in userList)
                    {
                        userModelList.Add(user.Id);
                    }
                }

            }
            else if (stepModel.FlowRule == "人员")
            {
                string[] array = stepModel.Execution.Split(',');
                foreach (var item in array)
                {
                    userModelList.Add(item);
                }
            }
            else if (stepModel.FlowRule == "自选")
            {
                string users = formContentBLL.GetById(formContentId).CustomMember;
                string[] array = users.Split(',');
                foreach (var item in array)
                {
                    userModelList.Add(item);
                }
            }
            return userModelList;
        }

        //对比
        private bool JudgeVal(string attrId, string rVal, string cVal, string lVal)
        {
            string attrType = formAttrBLL.GetById(attrId).AttrType;
            return new FlowHelper().Judge(attrType, rVal, cVal, lVal);
        }
        //无分页获取
        public GridPager setNoPagerAscBySort = new GridPager()
        {
            rows = 1000,
            page = 1,
            sort = "Sort",
            order = "asc"
        };
        //无分页获取
        public GridPager setNoPagerDescBySort = new GridPager()
        {
            rows = 1000,
            page = 1,
            sort = "Sort",
            order = "desc"
        };

    }
}