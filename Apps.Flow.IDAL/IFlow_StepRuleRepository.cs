using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_StepRuleRepository
    {
        IQueryable<Flow_StepRule> GetList(AppsDBEntities db);
        int Create(Flow_StepRule entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_StepRule entity);
        Flow_StepRule GetById(string id);
        bool IsExist(string id);
    }
}
