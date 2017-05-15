using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface IWC_OfficalAccountsRepository
    {

        IQueryable<WC_OfficalAccounts> GetList(AppsDBEntities db);
        int Create(WC_OfficalAccounts entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(WC_OfficalAccounts entity);
        WC_OfficalAccounts GetById(string id);
        WC_OfficalAccounts GetCurrentAccount();
        bool IsExist(string id);
    }
}
