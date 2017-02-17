using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface ISysModuleRepository
    {
        IQueryable<SysModule> GetList(AppsDBEntities db);
        IQueryable<SysModule> GetModuleBySystem(AppsDBEntities db, string parentId);
        int Create(SysModule entity);
        void Delete(AppsDBEntities db, string id);
        int Edit(SysModule entity);
        SysModule GetById(string id);
        bool IsExists(string id);

    }
}
