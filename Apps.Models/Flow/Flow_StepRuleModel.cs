using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_StepRuleModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "StepId")]
        public string StepId { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "AttrId")]
        public string AttrId { get; set; }

        [MaxWordsExpression(10)]
        [Display(Name = "Operator")]
        public string Operator { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "Result")]
        public string Result { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "NextStep")]
        public string NextStep { get; set; }
    }
}
