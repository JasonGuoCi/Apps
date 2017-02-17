using Apps.Common;
using Apps.Models.MIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.IBLL
{
    public interface IMIS_Article_CategoryBLL
    {
        List<MIS_Article_CategoryModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, MIS_Article_CategoryModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, MIS_Article_CategoryModel model);
        MIS_Article_CategoryModel GetById(string id);
        bool IsExist(string id);
    }
}
