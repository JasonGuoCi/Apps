using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface ISysLogRepository
    {
        int Create(SysLog entity);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Delete(string id);
        IQueryable<SysLog> GetList(AppsDBEntities db);
        SysLog GetById(string id);
    }
}
