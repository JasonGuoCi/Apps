using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_TypeModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "类别")]
        public string Name { get; set; }

        [MaxWordsExpression(500)]
        [Display(Name = "说明")]
        public string Remark { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "排序")]
        public int Sort { get; set; }

        public List<Flow_FormModel> formList { get; set; }

    }
}
