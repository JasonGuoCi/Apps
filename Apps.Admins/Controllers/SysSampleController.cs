using Apps.Admins.Core;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Controllers
{
    public class SysSampleController : BaseController
    {
        [Dependency]
        public ISysSampleBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        //[SupportFilter(ActionName = "Index")]
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysSampleModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysSampleModel()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Age = r.Age,
                            Bir = r.Bir,
                            Photo = r.Photo,
                            Note = r.Note,
                            CreateTime = r.CreateTime,

                        }).ToArray()

            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [SupportFilter]
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]

        public JsonResult Create(SysSampleModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog("虚拟用户", "ID:" + model.Id + ",Name:" + model.Name, "成功", "创建", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, "插入成功"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string errorsCor = errors.Error;
                    LogHandler.WriteServiceLog("虚拟用户", "ID:" + model.Id + ",Name:" + model.Name + "," + errorsCor, "失败", "创建", "样例程序");
                    return Json(JsonHandler.CreateMessage(0, "插入失败" + errorsCor), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }

        }
        #endregion

        #region 修改
        public ActionResult Edit(string id)
        {
            SysSampleModel model = m_BLL.GetById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(SysSampleModel model)
        {
            if (m_BLL.Edit(model))
            {
                LogHandler.WriteServiceLog("虚拟用户", "ID:" + model.Id + ",Name:" + model.Name, "成功", "修改", "样例程序");
                return Json(JsonHandler.CreateMessage(1, "修改成功"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                string errorsCor = errors.Error;
                LogHandler.WriteServiceLog("虚拟用户", "ID:" + model.Id + ",Name:" + model.Name + "," + errorsCor, "失败", "修改", "样例程序");
                return Json(JsonHandler.CreateMessage(0, "修改失败" + errorsCor), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 详细
        public ActionResult Details(string id)
        {
            SysSampleModel model = m_BLL.GetById(id);
            return View(model);
        }
        #endregion

        #region 删除
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(id))
                {
                    LogHandler.WriteServiceLog("虚拟用户", "ID:" + id, "成功", "删除", "样例程序");
                    return Json(JsonHandler.CreateMessage(1, "删除成功"), JsonRequestBehavior.AllowGet);
                }
                else
                {

                    string errorsCor = errors.Error;
                    LogHandler.WriteServiceLog("虚拟用户", "ID:" + id + "," + errorsCor, "失败", "删除", "样例程序");
                    return Json(JsonHandler.CreateMessage(0, "删除失败" + errorsCor), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string errorsCor = errors.Error;
                LogHandler.WriteServiceLog("虚拟用户", "ID:" + id + "," + errorsCor, "失败", "删除", "样例程序");
                return Json(JsonHandler.CreateMessage(0, "删除失败" + errorsCor), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region reporting
        //type = PDF，Excel，Word 
        public ActionResult Reporting(string type = "PDF", string queryStr = "", int rows = 0, int page = 1)
        {
            //选择了导出全部
            if (rows == 0 && page == 0)
            {
                rows = 9999999;
                page = 1;
            }

            GridPager pager = new GridPager()
            {
                rows = rows,
                page = page,
                sort = "Id",
                order = "desc"
            };

            List<SysSampleModel> ds = m_BLL.GetList(ref pager, queryStr);//把读取到的列表赋予给ds
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/SysSampleReport.rdlc");//指定报表的路径

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ds);//指定数据集 DataSet1
            localReport.DataSources.Add(reportDataSource);

            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "<OutPutFormat>" + type + "</OutPutFormat>" +
                 "<PageWidth>11in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0.5in</MarginTop>" +
                "<MarginLeft>1in</MarginLeft>" +
                "<MarginRight>1in</MarginRight>" +
                "<MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderBytes;

            renderBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderBytes, mimeType);

        }
        #endregion
    }
}