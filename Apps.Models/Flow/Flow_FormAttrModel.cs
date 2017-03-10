using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_FormAttrModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "字段标题")]
        public string Title { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "字段英文名称")]
        public string Name { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "文本,日期,数字,多行文本")]
        public string AttrType { get; set; }

        [MaxWordsExpression(500)]
        [Display(Name = "校验脚本")]
        public string CheckJS { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "所属类别")]
        public string TypeId { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        [MaxWordsExpression(500)]
        [Display(Name = "下拉框的值")]
        public string OptionList { get; set; }

        [Display(Name = "是否必填")]
        public bool IsValid { get; set; }
    }
}
