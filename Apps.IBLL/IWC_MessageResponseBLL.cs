using Apps.Common;
using Apps.Models.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface IWC_MessageResponseBLL
    {
        List<WC_MessageResponseModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, WC_MessageResponseModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, WC_MessageResponseModel model);
        WC_MessageResponseModel GetById(string id);
        bool IsExist(string id);
        bool PostData(ref ValidationErrors errors, WC_MessageResponseModel model);
    }
}
