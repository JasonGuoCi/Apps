using Apps.Flow.IDAL;

using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_FormContentStepCheckStateRepository : IFlow_FormContentStepCheckStateRepository, IDisposable
    {
        public IQueryable<Flow_FormContentStepCheckState> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_FormContentStepCheckState> list = db.Flow_FormContentStepCheckState.AsQueryable();
            return list;
        }

        public int Create(Flow_FormContentStepCheckState entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormContentStepCheckState.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormContentStepCheckState entity = db.Flow_FormContentStepCheckState.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_FormContentStepCheckState.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_FormContentStepCheckState> collection = from f in db.Flow_FormContentStepCheckState
                                                                    where deleteCollection.Contains(f.Id)
                                                                    select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_FormContentStepCheckState.Remove(deleteItem);
            }
        }

        public int Edit(Flow_FormContentStepCheckState entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormContentStepCheckState.Attach(entity);
                db.Entry<Flow_FormContentStepCheckState>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_FormContentStepCheckState GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_FormContentStepCheckState.SingleOrDefault(a => a.Id == id);
            }
        }
        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormContentStepCheckState entity = GetById(id);
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
