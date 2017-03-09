using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_StepRepository : IFlow_StepRepository, IDisposable
    {
        public IQueryable<Flow_Step> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_Step> list = db.Flow_Step.AsQueryable();
            return list;
        }

        public int Create(Flow_Step entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Step.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Step entity = db.Flow_Step.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_Step.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_Step> collection = from f in db.Flow_Step
                                               where deleteCollection.Contains(f.Id)
                                               select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_Step.Remove(deleteItem);
            }
        }

        public int Edit(Flow_Step entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Step.Attach(entity);
                db.Entry<Flow_Step>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_Step GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_Step.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Step entity = GetById(id);
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
