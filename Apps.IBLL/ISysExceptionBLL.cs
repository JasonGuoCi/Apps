using Apps.Common;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface ISysExceptionBLL
    {
        List<SysException> GetList(ref GridPager pager, string queryStr);

        bool Delete(string id);
        SysException GetById(string id);
    }
}
