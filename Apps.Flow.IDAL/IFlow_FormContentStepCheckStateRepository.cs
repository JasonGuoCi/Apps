using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_FormContentStepCheckStateRepository
    {
        IQueryable<Flow_FormContentStepCheckState> GetList(AppsDBEntities db);
        int Create(Flow_FormContentStepCheckState entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_FormContentStepCheckState entity);
        Flow_FormContentStepCheckState GetById(string id);
        bool IsExist(string id);
    }
}
