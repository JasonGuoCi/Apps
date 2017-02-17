using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class SysExceptionRepository : ISysExceptionRepository, IDisposable
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IQueryable<SysException> GetList(AppsDBEntities db)
        {
            IQueryable<SysException> list = db.SysException.AsQueryable();
            return list;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(SysException entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysException.Add(entity);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysException GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysException.SingleOrDefault(a => a.Id == id);
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysException entity = db.SysException.SingleOrDefault(a => a.Id == id);
                db.SysException.Remove(entity);
                return db.SaveChanges();
            }
        }
        public void Dispose() { }
    }
}
