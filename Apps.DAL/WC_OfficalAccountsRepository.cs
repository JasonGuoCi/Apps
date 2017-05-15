using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class WC_OfficalAccountsRepository : IWC_OfficalAccountsRepository, IDisposable
    {
        public IQueryable<WC_OfficalAccounts> GetList(AppsDBEntities db)
        {
            IQueryable<WC_OfficalAccounts> list = db.WC_OfficalAccounts.AsQueryable();
            return list;
        }

        public int Create(WC_OfficalAccounts entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.WC_OfficalAccounts.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                WC_OfficalAccounts entity = db.WC_OfficalAccounts.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.WC_OfficalAccounts.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<WC_OfficalAccounts> collection = from f in db.WC_OfficalAccounts
                                                        where deleteCollection.Contains(f.Id)
                                                        select f;
            foreach (var deleteItem in collection)
            {
                db.WC_OfficalAccounts.Remove(deleteItem);
            }
        }

        public int Edit(WC_OfficalAccounts entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.WC_OfficalAccounts.Attach(entity);
                db.Entry<WC_OfficalAccounts>(entity).State = EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public WC_OfficalAccounts GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.WC_OfficalAccounts.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                WC_OfficalAccounts entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public WC_OfficalAccounts GetCurrentAccount()
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.WC_OfficalAccounts.Where(p => p.IsDefault).FirstOrDefault();
            }
        }
        public void Dispose()
        {

        }
    }
}
