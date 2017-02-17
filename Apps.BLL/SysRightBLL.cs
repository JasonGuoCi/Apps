using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
    public class SysRightBLL : ISysRightBLL
    {
        [Dependency]
        public ISysRightRepository SysRightRepository { get; set; }
        public List<permModel> GetPermission(string accountid, string controllor)
        {
            return SysRightRepository.GetPermission(accountid, controllor);
        }

        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            return SysRightRepository.GetRightByRoleAndModule(roleId, moduleId);
        }

        public int UpdateRight(SysRightOperateModel model)
        {
            return SysRightRepository.UpdateRight(model);
        }
    }
}
