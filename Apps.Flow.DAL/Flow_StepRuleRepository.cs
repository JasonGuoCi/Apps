using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_StepRuleRepository : IFlow_StepRuleRepository, IDisposable
    {
        public IQueryable<Flow_StepRule> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_StepRule> list = db.Flow_StepRule.AsQueryable();
            return list;
        }

        public int Create(Flow_StepRule entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_StepRule.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_StepRule entity = db.Flow_StepRule.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_StepRule.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_StepRule> collection = from f in db.Flow_StepRule
                                                   where deleteCollection.Contains(f.Id)
                                                   select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_StepRule.Remove(deleteItem);
            }
        }

        public int Edit(Flow_StepRule entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_StepRule.Attach(entity);
                db.Entry<Flow_StepRule>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_StepRule GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_StepRule.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_StepRule entity = GetById(id);
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
