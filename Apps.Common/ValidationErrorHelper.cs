using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Common
{
    public class ValidationErrorHelper
    {
        public ValidationErrorHelper() { }
        public string ErrorMessage { get; set; }
    }

    public class ValidationErrors : List<ValidationErrorHelper>
    {
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="errorMsg">error描述</param>
        public void Add(string errorMsg)
        {
            base.Add(new ValidationErrorHelper { ErrorMessage = errorMsg });
        }

        public string Error
        {
            get
            {
                string error = "";
                this.All(a =>
                {
                    error += a.ErrorMessage;
                    return true;
                });
                return error;
            }
        }

    }
}
