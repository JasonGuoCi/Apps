using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_FormContentStepCheckRepository : IFlow_FormContentStepCheckRepository, IDisposable
    {
        public IQueryable<Flow_FormContentStepCheck> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_FormContentStepCheck> list = db.Flow_FormContentStepCheck.AsQueryable();
            return list;
        }

        public int Create(Flow_FormContentStepCheck entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormContentStepCheck.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormContentStepCheck entity = db.Flow_FormContentStepCheck.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_FormContentStepCheck.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_FormContentStepCheck> collection = from f in db.Flow_FormContentStepCheck
                                                               where deleteCollection.Contains(f.Id)
                                                               select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_FormContentStepCheck.Remove(deleteItem);
            }
        }

        public int Edit(Flow_FormContentStepCheck entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormContentStepCheck.Attach(entity);
                db.Entry<Flow_FormContentStepCheck>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
        public Flow_FormContentStepCheck GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_FormContentStepCheck.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormContentStepCheck entity = GetById(id);
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
