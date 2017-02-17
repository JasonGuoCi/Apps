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
    public class MIS_ArticleRepository : IMIS_ArticleRepository, IDisposable
    {
        public IQueryable<MIS_Article> GetList(AppsDBEntities db)
        {
            IQueryable<MIS_Article> list = db.MIS_Article.AsQueryable();
            return list;
        }

        public int Create(MIS_Article entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.MIS_Article.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                MIS_Article entity = db.MIS_Article.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.MIS_Article.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<MIS_Article> collection = from f in db.MIS_Article
                                                 where deleteCollection.Contains(f.Id)
                                                 select f;
            foreach (var deleteItem in collection)
            {
                db.MIS_Article.Remove(deleteItem);
            }
        }

        public int Edit(MIS_Article entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.MIS_Article.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Entry<MIS_Article>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public MIS_Article GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.MIS_Article.SingleOrDefault(a => a.Id == id);
            }
        }
        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                MIS_Article entity = GetById(id);
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
