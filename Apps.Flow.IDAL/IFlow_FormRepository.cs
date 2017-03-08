using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_FormRepository
    {
        IQueryable<Flow_Form> GetList(AppsDBEntities db);
        int Create(Flow_Form entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_Form entity);
        Flow_Form GetById(string id);
        bool IsExist(string id);
    }
}
