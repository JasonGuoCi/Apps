using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUser Login(string userName, string password)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysUser user = db.SysUser.Single(a => a.UserName == userName && a.Password == password);
                return user;
            }
        }

        public void Dispose()
        { }
    }
}
