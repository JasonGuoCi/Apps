using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Apps.DAL
{
    public class SysSampleRepository : ISysSampleRepository, IDisposable
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<SysSample> GetList(AppsDBEntities db)
        {
            IQueryable<SysSample> list = db.SysSample.AsQueryable();
            return list;
        }

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Create(SysSample entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Set<SysSample>().Add(entity);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns></returns>
        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysSample entity = db.SysSample.SingleOrDefault(a => a.Id == id);
                db.Set<SysSample>().Remove(entity);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Edit(SysSample entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.Set<SysSample>().Attach(entity);
                db.Entry<SysSample>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>实体</returns>
        public SysSample GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysSample.SingleOrDefault(a => a.Id == id);
            }
        }
        /// <summary>
        /// 
        ///判断一个实体是否存在
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>true/false</returns>
        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysSample entity = GetById(id);
                if (entity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Dispose()
        {

        }
    }
}
