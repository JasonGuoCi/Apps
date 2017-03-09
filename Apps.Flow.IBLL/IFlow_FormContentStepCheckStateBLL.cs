using Apps.Common;
using Apps.Models.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IBLL
{
    public interface IFlow_FormContentStepCheckStateBLL
    {
        List<Flow_FormContentStepCheckStateModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, Flow_FormContentStepCheckStateModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, Flow_FormContentStepCheckStateModel model);
        Flow_FormContentStepCheckStateModel GetById(string id);
        bool IsExist(string id);
    }
}
