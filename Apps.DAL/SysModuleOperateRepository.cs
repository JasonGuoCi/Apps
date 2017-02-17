using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class SysModuleOperateRepository : ISysModuleOperateRepository, IDisposable
    {
        public IQueryable<SysModuleOperate> GetList(AppsDBEntities db)
        {
            IQueryable<SysModuleOperate> list = db.SysModuleOperate.AsQueryable();
            return list;
        }

        public int Create(SysModuleOperate entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysModuleOperate.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysModuleOperate entity = db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    db.SysModuleOperate.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public SysModuleOperate GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysModuleOperate entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }
    }
}
