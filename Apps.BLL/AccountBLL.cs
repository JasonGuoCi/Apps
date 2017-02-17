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
    public class AccountBLL : BaseBLL, IAccountBLL
    {
        [Dependency]
        public IAccountRepository accountRepository { get; set; }

        public SysUser Login(string userName, string password)
        {
            return accountRepository.Login(userName, password);
        }
    }
}
