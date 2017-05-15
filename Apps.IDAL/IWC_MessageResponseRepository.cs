using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface IWC_MessageResponseRepository
    {
        IQueryable<WC_MessageResponse> GetList(AppsDBEntities db);
        int Create(WC_MessageResponse entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(WC_MessageResponse entity);
        WC_MessageResponse GetById(string id);
        bool IsExist(string id);
        bool PostData(WC_MessageResponse model);
    }
}
