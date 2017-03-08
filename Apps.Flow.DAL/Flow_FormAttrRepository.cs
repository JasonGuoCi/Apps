using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_FormAttrRepository : IFlow_FormAttrRepository, IDisposable
    {
        public IQueryable<Flow_FormAttr> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_FormAttr> list = db.Flow_FormAttr.AsQueryable();
            return list;
        }

        public int Create(Flow_FormAttr entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormAttr.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormAttr entity = db.Flow_FormAttr.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_FormAttr.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_FormAttr> collection = from f in db.Flow_FormAttr
                                                   where deleteCollection.Contains(f.Id)
                                                   select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_FormAttr.Remove(deleteItem);
            }
        }

        public int Edit(Flow_FormAttr entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_FormAttr.Attach(entity);
                db.Entry<Flow_FormAttr>(entity).State = System.Data.Entity.EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_FormAttr GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_FormAttr.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_FormAttr entity = GetById(id);
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
