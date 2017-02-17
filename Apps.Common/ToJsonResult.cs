using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Apps.Common
{
    public class ToJsonResult : JsonResult
    {
        const string error = "该请求已被封锁，因为敏感信息透露给第三方网站，这是一个GET请求时使用的。为了可以GET请求，请设置JsonRequestBeavior AllowGET。";
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormateStr { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            //base.ExecuteResult(context);
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(error);
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonString = serializer.Serialize(Data);

                string p = @"\\/Date\(\d+\)\\/";

                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDataToDataString);
                Regex reg = new Regex(p);

                jsonString = reg.Replace(jsonString, matchEvaluator);
                response.Write(jsonString);
            }
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string ConvertJsonDataToDataString(Match m)
        {
            //throw new NotImplementedException();

            string result = string.Empty;

            string p = @"\d";
            var cArray = m.Value.ToCharArray();
            StringBuilder sb = new StringBuilder();

            Regex reg = new Regex(p);

            for (int i = 0; i < cArray.Length; i++)
            {
                if (reg.IsMatch(cArray[i].ToString()))
                {
                    sb.Append(cArray[i]);
                }
            }

            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(sb.ToString()));
            dt = dt.ToLocalTime();

            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
    }
}
