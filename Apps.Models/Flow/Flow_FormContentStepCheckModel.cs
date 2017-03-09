using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_FormContentStepCheckModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "所属公文")]
        public string ContentId { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "StepId")]
        public string StepId { get; set; }

        [Display(Name = "0不通过1通过2审核中")]
        public int State { get; set; }

        [Display(Name = "true此步骤审核完成")]
        public bool StateFlag { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "IsEnd")]
        public bool IsEnd { get; set; }
    }
}
