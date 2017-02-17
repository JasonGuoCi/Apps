using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models;
using System.Data.Entity;

namespace Apps.BLL
{
    public class BaseBLL : IDisposable
    {
        private AppsDBEntities _db = new AppsDBEntities();

        public AppsDBEntities db
        {
            get { return _db; }
        }
        public void Dispose() { }
    }

}
