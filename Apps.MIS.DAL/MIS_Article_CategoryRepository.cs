using Apps.MIS.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.DAL
{
    public class MIS_Article_CategoryRepository : IMIS_Article_CategoryRepository, IDisposable
    {
        public IQueryable<MIS_Article_Category> GetList(AppsDBEntities db)
        {
            IQueryable<MIS_Article_Category> list = db.MIS_Article_Category.AsQueryable();
            return list;
        }

        public int Create(MIS_Article_Category entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.MIS_Article_Category.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                MIS_Article_Category entity = db.MIS_Article_Category.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.MIS_Article_Category.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<MIS_Article_Category> collection = from f in db.MIS_Article_Category
                                                          where deleteCollection.Contains(f.Id)
                                                          select f;
            foreach (var deleteItem in collection)
            {
                db.MIS_Article_Category.Remove(deleteItem);
            }
        }

        public int Edit(MIS_Article_Category entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.MIS_Article_Category.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Entry<MIS_Article_Category>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public MIS_Article_Category GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.MIS_Article_Category.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                MIS_Article_Category entity = GetById(id);
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
