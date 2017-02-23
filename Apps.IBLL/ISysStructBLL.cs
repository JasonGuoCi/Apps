using Apps.Common;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface ISysStructBLL
    {
        List<SysStructModel> GetList();
        List<SysStructModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysStructModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, SysStructModel model);
        SysStructModel GetById(string id);
        bool IsExist(string id);
    }
}
