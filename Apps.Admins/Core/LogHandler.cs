using Apps.Common;
using Apps.DAL;
using Apps.IBLL;
using Apps.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Admins.Core
{
    public static class LogHandler
    {
        [Dependency]

        public static ISysLogBLL logBLL { get; set; }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="oper">操作人</param>
        /// <param name="msg">操作信息</param>
        /// <param name="result">结构</param>
        /// <param name="type">类型</param>
        /// <param name="module">操作模块</param>
        public static void WriteServiceLog(string oper, string msg, string result, string type, string module)
        {
            SysLog entity = new SysLog();
            entity.Id = ResultHelper.NewId;
            entity.Operator = oper;
            entity.Message = msg;
            entity.Result = result;
            entity.Type = type;
            entity.Module = module;
            entity.CreateTime = ResultHelper.NowTime;

            using (SysLogRepository logRepository = new SysLogRepository())
            {
                logRepository.Create(entity);
            }

        }
    }
}