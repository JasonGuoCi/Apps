using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps.Models.Flow;

namespace Apps.Admins.Core
{
    public class FlowHelper
    {
        //获取指定类型的html表单
        public string GetInput(string type, string id, string attrNo)
        {
            string str = "";
            if (type == "文本")
            {
                str = "<input id='" + id + "' name='" + attrNo + "' type='text'/>";
            }
            else if (type == "多行文本")
            {
                str = "<textarea id='" + id + "' name='" + attrNo + "' cols='60' style='height:80px;' row='5'></textarea>";
            }
            else if (type == "日期")
            {
                str = "<input type='text' name='" + attrNo + "' class='Wdate' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd'})\" id='" + id + "' />";
            }
            else if (type == "时间")
            {
                str = "<input type='text'  name='" + attrNo + "' class='Wdate' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})\"  id='" + id + "'  />";
            }
            else if (type == "数字")
            {
                str = "<input type='number' name='" + attrNo + "' id='" + id + "' />";
            }
            return str;

        }

        internal string GetFormAttrVal(string attrId, Flow_FormModel formModel, Flow_FormContentModel model)
        {
            throw new NotImplementedException();
        }

        internal bool Judge(string attrType, string rVal, string cVal, string lVal)
        {
            throw new NotImplementedException();
        }
    }
}