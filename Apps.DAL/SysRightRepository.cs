using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class SysRightRepository : ISysRightRepository, IDisposable
    {
        /// <summary>
        /// 取角色模块的操作权限，用于权限控制
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public List<permModel> GetPermission(string accountId, string controller)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                List<permModel> rights = (
                    from r in db.P_Sys_GetRightOperate(accountId, controller)
                    select new permModel
                    {
                        KeyCode = r.KeyCode,
                        IsValid = r.IsValid
                    }).ToList();

                return rights;
            }
        }

        public int UpdateRight(SysRightOperateModel model)
        {
            //转换
            SysRightOperate rightOperate = new SysRightOperate();
            rightOperate.Id = model.Id;
            rightOperate.RightId = model.RightId;
            rightOperate.KeyCode = model.KeyCode;
            rightOperate.IsValid = model.IsValid;

            //判断rightOperate是否存在，如果存在就更新rightOperate,否则就添加一条
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysRightOperate right = db.SysRightOperate.Where(a => a.Id == rightOperate.Id).FirstOrDefault();
                if (right != null)
                {
                    right.IsValid = rightOperate.IsValid;
                }
                else
                {
                    db.SysRightOperate.Add(rightOperate);
                }

                if (db.SaveChanges() > 0)
                {
                    //更新角色--模块的有效标志RightFlag
                    var sysRight = (from r in db.SysRight
                                    where r.Id == rightOperate.RightId
                                    select r).First();
                    db.P_Sys_UpdateSysRightRightFlag(sysRight.ModuleId, sysRight.RoleId);
                    return 1;
                }
            }
            return 0;
        }

        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            List<P_Sys_GetRightByRoleAndModule_Result> result = null;
            using (AppsDBEntities db = new AppsDBEntities())
            {
                result = db.P_Sys_GetRightByRoleAndModule(roleId, moduleId).ToList();
            }
            return result;
        }
        public void Dispose() { }
    }
}
