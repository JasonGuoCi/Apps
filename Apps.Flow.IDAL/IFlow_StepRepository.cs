using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_StepRepository
    {
        IQueryable<Flow_Step> GetList(AppsDBEntities db);
        int Create(Flow_Step entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_Step entity);
        Flow_Step GetById(string id);
        bool IsExist(string id);
    }
}
