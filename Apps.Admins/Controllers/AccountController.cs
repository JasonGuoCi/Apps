using Apps.Admins.Core;
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
    public class AccountController : BaseController
    {
        [Dependency]
        public IAccountBLL accoutBLL { get; set; }


        // GET: Account
        public ActionResult Index()
        {
            //AccountModel account = new AccountModel();
            //account.Id = "admin";
            //account.TrueName = "admin";
            //Session["Account"] = account;

            return View();
        }

        public JsonResult Login(string userName, string password, string Code)
        {
            //验证验证码
            if (Session["Code"] == null)
            {
                return Json(JsonHandler.CreateMessage(0, "请重新刷新验证码"), JsonRequestBehavior.AllowGet);

            }
            if (Session["Code"].ToString().ToLower() != Code.ToLower())
            {
                return Json(JsonHandler.CreateMessage(0, "验证码错误"), JsonRequestBehavior.AllowGet);
            }

            //验证用户
            SysUser user = accoutBLL.Login(userName, ValueConvert.MD5(password));
            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            }

            else if (!Convert.ToBoolean(user.State))//被禁用
            {
                return Json(JsonHandler.CreateMessage(0, "用户被系统禁用"), JsonRequestBehavior.AllowGet);
            }

            AccountModel account = new AccountModel();
            account.Id = user.Id;
            account.TrueName = user.TrueName;
            Session["Account"] = account;

            //在线用户统计
            //OnlineHttpModule.ProcessRequest();

            return Json(JsonHandler.CreateMessage(1, ""), JsonRequestBehavior.AllowGet);
        }


    }
}