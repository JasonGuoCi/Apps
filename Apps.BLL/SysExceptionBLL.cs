using Apps.Common;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
    public class SysExceptionBLL : ISysExceptionBLL
    {
        [Dependency]

        public ISysExceptionRepository exceptionRepository { get; set; }

        public List<SysException> GetList(ref GridPager pager, string queryStr)
        {
            AppsDBEntities db = new AppsDBEntities();
            List<SysException> query = null;
            IQueryable<SysException> list = exceptionRepository.GetList(db);

            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }
            if (pager.order == "desc")
            {
                query = list.OrderByDescending(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }
            else
            {
                query = list.OrderBy(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            }
            return query;
        }

        public SysException GetById(string id)
        {
            return exceptionRepository.GetById(id);
        }

        public bool Delete(string id)
        {
            try
            {
                if (exceptionRepository.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }
    }
}
