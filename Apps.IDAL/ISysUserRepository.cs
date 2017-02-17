using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface ISysUserRepository
    {
        IQueryable<SysUser> GetList(AppsDBEntities db);
        int Create(SysUser entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(SysUser entity);
        SysUser GetById(string id);
        bool IsExist(string id);
        IQueryable<P_Sys_GetRoleByUserId_Result> GetRoleByUserId(AppsDBEntities db, string userId);
        void UpdateSysRoleSysUser(string userId, string[] roleIds);
    }
}
