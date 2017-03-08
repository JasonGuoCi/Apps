using Apps.Common;
using Apps.Models.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IBLL
{
    public interface IFlow_FormContentBLL
    {
        List<Flow_FormContentModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, Flow_FormContentModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, Flow_FormContentModel model);
        Flow_FormContentModel GetById(string id);
        bool IsExist(string id);
    }
}
