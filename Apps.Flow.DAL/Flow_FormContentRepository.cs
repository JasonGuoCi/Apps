using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_FormContentRepository : IFlow_FormContentRepository, IDisposable
    {
        public IQueryable<Flow_FormContent> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_FormContent> list = db.Flow_FormContent.AsQueryable();
            return list;
        }

        public int Create(Flow_FormContent entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormContent.Add(entity);
                return db.SaveChanges();
            }
        }
        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormContent entity = db.Flow_FormContent.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_FormContent.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_FormContent> collection = from f in db.Flow_FormContent
                                                      where deleteCollection.Contains(f.Id)
                                                      select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_FormContent.Remove(deleteItem);
            }
        }

        public int Edit(Flow_FormContent entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormContent.Attach(entity);
                db.Entry<Flow_FormContent>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }
        public Flow_FormContent GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_FormContent.SingleOrDefault(a => a.Id == id);
            }
        }
        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormContent entity = GetById(id);
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
