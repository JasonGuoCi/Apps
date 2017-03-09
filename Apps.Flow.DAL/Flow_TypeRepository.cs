using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_TypeRepository : IFlow_TypeRepository, IDisposable
    {
        public IQueryable<Flow_Type> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_Type> list = db.Flow_Type.AsQueryable();
            return list;
        }

        public int Create(Flow_Type entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Type.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Type entity = db.Flow_Type.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_Type.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_Type> collection = from f in db.Flow_Type
                                               where deleteCollection.Contains(f.Id)
                                               select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_Type.Remove(deleteItem);
            }
        }

        public int Edit(Flow_Type entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Type.Attach(entity);
                db.Entry<Flow_Type>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_Type GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_Type.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Type entity = GetById(id);
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
