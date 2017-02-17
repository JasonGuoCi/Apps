using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface ISysRoleBLL
    {
        List<SysRoleModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysRoleModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Edit(ref ValidationErrors errors, SysRoleModel model);
        SysRoleModel GetById(string id);
        bool IsExist(string id);

        string GetRefSysUser(string roleId);

        IQueryable<P_Sys_GetUserByRoleId_Result> GetUserByRoleId(ref GridPager pager, string roleId);
        bool UpdateSysRoleSysUser(string roleId, string[] userIds);
    }
}
