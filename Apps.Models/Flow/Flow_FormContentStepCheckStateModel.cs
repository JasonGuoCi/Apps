using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_FormContentStepCheckStateModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "StepCheckId")]
        public string StepCheckId { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Display(Name = "1通过0不通过2审核中")]
        public int CheckFlag { get; set; }

        [MaxWordsExpression(2000)]
        [Display(Name = "Reamrk")]
        public string Reamrk { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "TheSeal")]
        public string TheSeal { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime CreateTime { get; set; }
    }
}
