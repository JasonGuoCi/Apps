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
    public class SysLogBLL : ISysLogBLL
    {
        [Dependency]

        public ISysLogRepository logRepository { get; set; }

        public List<SysLog> GetList(ref GridPager pager, string queryStr)
        {
            AppsDBEntities db = new AppsDBEntities();
            List<SysLog> query = null;

            IQueryable<SysLog> list = logRepository.GetList(db);

            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr) || a.Module.Contains(queryStr));
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

        public SysLog GetById(string id)
        {
            return logRepository.GetById(id);
        }


        public bool Delete(string id)
        {
            try
            {
                if (logRepository.Delete(id) == 1)
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
