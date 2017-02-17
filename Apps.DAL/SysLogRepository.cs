using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class SysLogRepository : ISysLogRepository, IDisposable
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <returns>集合</returns>
        public IQueryable<SysLog> GetList(AppsDBEntities db)
        {
            IQueryable<SysLog> list = db.SysLog.AsQueryable();
            return list;
        }
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(SysLog entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysLog.Add(entity);//db.SysLog.AddObject(entity); 博客里面这么写，代码报错，没有addObject
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="deleteCollection">删除集合k</param>
        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<SysLog> collection = from f in db.SysLog
                                            where deleteCollection.Contains(f.Id)
                                            select f;
            foreach (var item in collection)
            {
                db.SysLog.Remove(item);//博客这么写db.SysLog.DeleteObject(deleteItem);
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysLog entity = db.SysLog.SingleOrDefault(a => a.Id == id);

                db.SysLog.Remove(entity);
                return db.SaveChanges();

            }
        }

        /// <summary>
        ///  根据ID获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysLog GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysLog.SingleOrDefault(a => a.Id == id);
            }
        }
        public void Dispose()
        {

        }
    }
}
