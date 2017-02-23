using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class SysStructModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "上级ID")]
        public string ParentId { get; set; }

        [Display(Name = "排序")]
        public int Sort { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "Higher")]
        public string Higher { get; set; }

        [Display(Name = "是否启用")]
        public bool Enable { get; set; }

        [MaxWordsExpression(500)]
        [Display(Name = "说明")]
        public string Remark { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
