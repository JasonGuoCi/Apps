using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_SealRepository : IFlow_SealRepository, IDisposable
    {
        public IQueryable<Flow_Seal> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_Seal> list = db.Flow_Seal.AsQueryable();
            return list;
        }
        public int Create(Flow_Seal entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Seal.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Seal entity = db.Flow_Seal.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_Seal.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_Seal> collection = from f in db.Flow_Seal
                                               where deleteCollection.Contains(f.Id)
                                               select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_Seal.Remove(deleteItem);
            }
        }

        public int Edit(Flow_Seal entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Seal.Attach(entity);
                db.Entry<Flow_Seal>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_Seal GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_Seal.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Seal entity = GetById(id);
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
