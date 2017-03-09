using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_TypeRepository
    {
        IQueryable<Flow_Type> GetList(AppsDBEntities db);
        int Create(Flow_Type entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_Type entity);
        Flow_Type GetById(string id);
        bool IsExist(string id);
    }
}
