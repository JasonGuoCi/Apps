using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.IDAL
{
    public interface IFlow_FormAttrRepository
    {
        IQueryable<Flow_FormAttr> GetList(AppsDBEntities db);
        int Create(Flow_FormAttr entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(Flow_FormAttr entity);
        Flow_FormAttr GetById(string id);
        bool IsExist(string id);
    }
}
