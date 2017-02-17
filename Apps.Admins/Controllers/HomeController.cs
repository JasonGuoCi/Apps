using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        [Dependency]
        public IHomeBLL homeBLL { get; set; }
        [Dependency]
        public ISysModuleBLL m_BLL { get; set; }
        public ActionResult Index()
        {
            //Convert.ToInt16("dddd");
            return View();
        }

        public JsonResult GetTree(string id)
        {
            if (Session["Account"] != null)
            {
                AccountModel account = (AccountModel)Session["Account"];
                List<SysModule> menus = homeBLL.GetMenuByPersonId(account.Id, id);

                var jsonData = (
                    from m in menus
                    select new
                    {
                        id = m.Id,
                        text = m.Name,
                        value = m.Url,
                        showcheck = false,
                        complete = false,
                        isexpand = false,
                        checkstate = 0,
                        hasChildren = m.IsLast ? false : true,
                        Icon = m.Iconic
                    }).ToArray();
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTreeByEasyui(string id)
        {
            //加入本地化
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            string infoName = info.Name;
            if (Session["Account"] != null)
            {
                //加入本地化
                AccountModel account = (AccountModel)Session["Account"];
                //List<SysModuleModel> list = homeBLL.GetMenuByPersonId(account.Id, id);
                List<SysModule> list = homeBLL.GetMenuByPersonId(account.Id, id);
                var json = from r in list
                           select new SysModuleNavModel()
                           {
                               id = r.Id,
                               text = infoName.IndexOf("zh") > -1 || infoName == "" ? r.Name : r.EnglishName,     //text
                               attributes = (infoName.IndexOf("zh") > -1 || infoName == "" ? "zh-CN" : "en-US") + "/" + r.Url,
                               iconCls = r.Iconic,
                               state = (m_BLL.GetList(r.Id).Count > 0) ? "closed" : "open"

                           };


                return Json(json);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

    }
}