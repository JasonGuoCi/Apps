using Apps.Flow.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Flow.DAL
{
    public class Flow_FormRepository : IFlow_FormRepository, IDisposable
    {
        public IQueryable<Flow_Form> GetList(AppsDBEntities db)
        {
            IQueryable<Flow_Form> list = db.Flow_Form.AsQueryable();
            return list;
        }

        public int Create(Flow_Form entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Form.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Form entity = db.Flow_Form.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.Flow_Form.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<Flow_Form> collection = from f in db.Flow_Form
                                               where deleteCollection.Contains(f.Id)
                                               select f;
            foreach (var deleteItem in collection)
            {
                db.Flow_Form.Remove(deleteItem);
            }
        }

        public int Edit(Flow_Form entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Flow_Form.Attach(entity);

                db.Entry<Flow_Form>(entity).State = EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public Flow_Form GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Flow_Form.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                Flow_Form entity = GetById(id);
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
