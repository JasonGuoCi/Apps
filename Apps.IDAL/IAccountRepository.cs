using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public interface IAccountRepository
    {
        SysUser Login(string userName, string password);
    }
}
