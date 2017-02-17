using Apps.Common;
using Apps.Models.MIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.IBLL
{
    public interface IMIS_ArticleBLL
    {
        List<MIS_ArticleModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, MIS_ArticleModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, MIS_ArticleModel model);
        MIS_ArticleModel GetById(string id);
        bool IsExist(string id);
    }
}
