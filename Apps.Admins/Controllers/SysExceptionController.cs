using Apps.Common;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Controllers
{
    public class SysExceptionController : Controller
    {
        [Dependency]
        public ISysExceptionBLL exceptionBLL { get; set; }
        // GET: SysException
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            BaseException ex = new BaseException();
            return View(ex);
        }
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysException> list = exceptionBLL.GetList(ref pager, queryStr);

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysException()
                        {
                            Id = r.Id,
                            HelpLink = r.HelpLink,
                            Message = r.Message,
                            Source = r.Source,
                            StackTrace = r.StackTrace,
                            TargetSite = r.TargetSite,
                            Data = r.Data,
                            CreateTime = r.CreateTime
                        }).ToArray()
            };

            return Json(json);
        }

        #region details
        public ActionResult Details(string id)
        {
            SysException entity = exceptionBLL.GetById(id);
            SysExceptionModel info = new SysExceptionModel()
            {
                Id = entity.Id,
                HelpLink = entity.HelpLink,
                Message = entity.Message,
                Source = entity.Source,
                StackTrace = entity.StackTrace,
                TargetSite = entity.TargetSite,
                Data = entity.Data,
                CreateTime = entity.CreateTime,
            };

            return View(info);
        }
        #endregion

        #region Delete
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (exceptionBLL.Delete(id))
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}