using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_FormContentStepCheckRepository
    {
        IQueryable<Flow_FormContentStepCheck> GetList(AppsDBEntities db);
        int Create(Flow_FormContentStepCheck entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_FormContentStepCheck entity);
        Flow_FormContentStepCheck GetById(string id);
        bool IsExist(string id);
    }
}
