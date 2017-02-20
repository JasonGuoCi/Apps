using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class SysStructRepository : ISysStructRepository, IDisposable
    {
        public IQueryable<SysStruct> GetList(AppsDBEntities db)
        {
            IQueryable<SysStruct> list = db.SysStruct.AsQueryable();
            return list;
        }

        public int Create(SysStruct entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysStruct.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysStruct entity = db.SysStruct.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.SysStruct.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<SysStruct> collection = from f in db.SysStruct
                                               where deleteCollection.Contains(f.Id)
                                               select f;
            foreach (var deleteItem in collection)
            {
                db.SysStruct.Remove(deleteItem);
            }
        }

        public int Edit(SysStruct entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysStruct.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Entry<SysStruct>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        public SysStruct GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysStruct.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysStruct entity = GetById(id);
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
