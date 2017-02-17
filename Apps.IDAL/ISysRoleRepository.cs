using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface ISysRoleRepository
    {
        IQueryable<SysRole> GetList(AppsDBEntities db);
        int Create(SysRole entity);
        int Delete(string id);
        int Edit(SysRole entity);
        SysRole GetById(string id);
        bool IsExist(string id);

        IQueryable<SysUser> GetRefSysUser(AppsDBEntities db, string id);
        IQueryable<P_Sys_GetUserByRoleId_Result> GetUserByRoleId(AppsDBEntities db, string roleId);

        void UpdateSysRoleSysUser(string roleId, string[] userIds);
    }
}
