using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MIS.IDAL
{
    public interface IMIS_ArticleRepository
    {
        IQueryable<MIS_Article> GetList(AppsDBEntities db);
        int Create(MIS_Article entity);
        int Delete(string id);
        void Delete(AppsDBEntities db, string[] deleteCollection);
        int Edit(MIS_Article entity);
        MIS_Article GetById(string id);
        bool IsExist(string id);
    }
}
