
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.WebChat.Core
{
    public class BaseRepository
    {
        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Database.ExecuteSqlCommand(sql);
            }
        }
        /// <summary>
        /// 异步执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<int> ExecuteSqlCommandAsync(string sql)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Database.ExecuteSqlCommandAsync(sql);
            }

        }

        //public DbRawSqlQuery<T> SqlQuery(string sql)
        //{
        //    using (AppsDBEntities db = new AppsDBEntities())
        //    {
        //        return db.Database.SqlQuery<T>(sql);
        //    }
        //}
        /// <summary>
        /// 查询一条语句返回结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        //public DbRawSqlQuery<T> SqlQuery(string sql, params object[] paras)
        //{
        //    using (AppsDBEntities db = new AppsDBEntities())
        //    {
        //        return db.Database.SqlQuery<T>(sql, paras);
        //    }
        //}
    }
}
