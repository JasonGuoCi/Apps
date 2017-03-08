using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_FormContentRepository
    {
        IQueryable<Flow_FormContent> GetList(AppsDBEntities db);
        int Create(Flow_FormContent entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_FormContent entity);
        Flow_FormContent GetById(string id);
        bool IsExist(string id);
    }
}
