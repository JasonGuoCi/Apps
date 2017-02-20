using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface ISysStructRepository
    {
        IQueryable<SysStruct> GetList(AppsDBEntities db);
        int Create(SysStruct entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(SysStruct entity);
        SysStruct GetById(string id);
        bool IsExist(string id);
    }
}
