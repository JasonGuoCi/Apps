using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Apps.DAL
{
    public class SysModuleRepository : ISysModuleRepository, IDisposable
    {
        public IQueryable<SysModule> GetList(AppsDBEntities db)
        {
            IQueryable<SysModule> list = db.SysModule.AsQueryable();
            return list;
        }

        public IQueryable<SysModule> GetModuleBySystem(AppsDBEntities db, string parentId)
        {
            return db.SysModule.Where(a => a.ParentId == parentId).AsQueryable();
        }

        public int Create(SysModule entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysModule.Add(entity);
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string id)
        {
            SysModule entity = db.SysModule.SingleOrDefault(a => a.Id == id);

            if (entity != null)
            {
                //删除SysRight表数据
                var sr = db.SysRight.AsQueryable().Where(a => a.ModuleId == id);
                foreach (var item in sr)
                {
                    //删除SysRightOperate表数据
                    var sro = db.SysRightOperate.AsQueryable().Where(a => a.RightId == item.Id);
                    foreach (var o in sro)
                    {
                        db.SysRightOperate.Remove(o);
                    }
                    db.SysRight.Remove(item);
                }

                //删除SysModuleOperate数据
                var smo = db.SysModuleOperate.AsQueryable().Where(a => a.ModuleId == id);
                foreach (var item in smo)
                {
                    db.SysModuleOperate.Remove(item);
                }
                db.SysModule.Remove(entity);
            }
        }

        public int Edit(SysModule entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysModule.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                // db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Entry<SysModule>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        public SysModule GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysModule.SingleOrDefault(a => a.Id == id);

            }
        }
        public bool IsExists(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysModule entity = GetById(id);
                if (entity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Dispose() { }
    }
}
