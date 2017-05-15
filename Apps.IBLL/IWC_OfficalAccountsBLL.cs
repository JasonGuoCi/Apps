using Apps.Common;
using Apps.Models.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface IWC_OfficalAccountsBLL
    {

        List<WC_OfficalAccountsModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, WC_OfficalAccountsModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, WC_OfficalAccountsModel model);
        WC_OfficalAccountsModel GetById(string id);
        WC_OfficalAccountsModel GetCurrentAccount();
        bool IsExist(string id);
    }
}
