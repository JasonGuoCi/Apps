using Apps.Admins.Core;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Admins.Controllers
{
    public class SysUserController : BaseController
    {
        // GET: SysUser
        [Dependency]
        public ISysUserBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysUserModel> list = m_BLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysUserModel()
                        {

                            Id = r.Id,
                            UserName = r.UserName,
                            Password = r.Password,
                            TrueName = r.TrueName,
                            Card = r.Card,
                            MobileNumber = r.MobileNumber,
                            PhoneNumber = r.PhoneNumber,
                            QQ = r.QQ,
                            EmailAddress = r.EmailAddress,
                            OtherContact = r.OtherContact,
                            Province = r.Province,
                            City = r.City,
                            Village = r.Village,
                            Address = r.Address,
                            State = r.State,
                            CreateTime = r.CreateTime,
                            CreatePerson = r.CreatePerson,
                            Sex = r.Sex,
                            Birthday = r.Birthday,
                            JoinDate = r.JoinDate,
                            Marital = r.Marital,
                            Political = r.Political,
                            Nationality = r.Nationality,
                            Native = r.Native,
                            School = r.School,
                            Professional = r.Professional,
                            Degree = r.Degree,
                            DepId = r.DepId,
                            PosId = r.PosId,
                            Expertise = r.Expertise,
                            JobState = r.JobState,
                            Photo = r.Photo,
                            Attach = r.Attach
                           // Flag = r.Flag

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
        public JsonResult Create(SysUserModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName, "成功", "创建", "SysUser");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName + "," + ErrorCol, "失败", "创建", "SysUser");
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
            SysUserModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysUserModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName, "成功", "修改", "SysUser");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserName" + model.UserName + "," + ErrorCol, "失败", "修改", "SysUser");
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
            SysUserModel entity = m_BLL.GetById(id);
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "SysUser");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "SysUser");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }


        }
        #endregion

        #region 设置用户角色
        [SupportFilter(ActionName = "Allot")]
        public ActionResult GetRoleByUser(string userId)
        {
            ViewBag.UserId = userId;
            ViewBag.Perm = GetPermission();
            return View();
        }
        [SupportFilter(ActionName = "Allot")]
        public JsonResult GetRoleListByUser(GridPager pager, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Json(0);
            }

            var userList = m_BLL.GetRoleByUserId(ref pager, userId);
            var jsonData = new
            {
                total = pager.totalRows,
                rows = (
                    from r in userList
                    select new SysRoleModel()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Flag = r.flag == "0" ? "0" : "1",
                    }
                ).ToArray()
            };
            return Json(jsonData);
        }
        #endregion

        [SupportFilter(ActionName = "Save")]
        public JsonResult UpdateUserRoleByUserId(string userId, string roleIds)
        {
            string[] arr = roleIds.Split(',');


            if (m_BLL.UpdateSysRoleSysUser(userId, arr))
            {
                LogHandler.WriteServiceLog(GetUserId(), "Ids:" + roleIds, "成功", "分配角色", "用户设置");
                return Json(JsonHandler.CreateMessage(1, Suggestion.SetSucceed), JsonRequestBehavior.AllowGet);
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "Ids:" + roleIds, "失败", "分配角色", "用户设置");
                return Json(JsonHandler.CreateMessage(0, Suggestion.SetFail), JsonRequestBehavior.AllowGet);
            }

        }
    }
}