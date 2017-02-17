using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface ISysModuleOperateRepository
    {
        IQueryable<SysModuleOperate> GetList(AppsDBEntities db);
        int Create(SysModuleOperate entity);
        int Delete(string id);
        SysModuleOperate GetById(string id);
        bool IsExist(string id);
    }
}
