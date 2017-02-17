using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Apps.BLL.Core
{
    public static class ExceptionHander
    {
        /// <summary>
        /// 写异常
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteException(Exception ex)
        {
            try
            {
                using (AppsDBEntities db = new AppsDBEntities())
                {
                    SysException model = new SysException()
                    {
                        Id = ResultHelper.NewId,
                        HelpLink = ex.HelpLink,
                        Message = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        TargetSite = ex.TargetSite.ToString(),
                        Data = ex.Data.ToString(),
                        CreateTime = ResultHelper.NowTime
                    };

                    db.SysException.Add(model);
                    db.SaveChanges();
                }
            }
            catch (Exception ep)
            {

                // throw;
                try
                {
                    string path = @"~/exceptionLog.txt";
                    string txtPath = HttpContext.Current.Server.MapPath(path);//获取绝对路径

                    using (StreamWriter sw = new StreamWriter(txtPath, true, Encoding.Default))
                    {
                        sw.WriteLine((ex.Message + "|" + ex.StackTrace + "|" + ep.Message + "|" + DateTime.Now.ToString()).ToString());
                        sw.Dispose();
                        sw.Close();
                    }
                    return;
                }
                catch (Exception)
                {
                    return;
                    //throw;
                }
            }
        }
    }
}
