using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_SealRepository
    {
        IQueryable<Flow_Seal> GetList(AppsDBEntities db);
        int Create(Flow_Seal entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_Seal entity);
        Flow_Seal GetById(string id);
        bool IsExist(string id);
    }
}
