using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface IHomeBLL
    {
        //List<SysModule> GetMenuByPersonId(string moduleId);
        List<SysModule> GetMenuByPersonId(string personId, string moduleId);
        //List<SysModuleModel> GetMenuByPersonId2(string personId, string moduleId);
    }
}
