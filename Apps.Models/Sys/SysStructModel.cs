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
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "ParentId")]
        public string ParentId { get; set; }

        [Display(Name = "Sort")]
        public int Sort { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "Higher")]
        public string Higher { get; set; }

        [Display(Name = "Enable")]
        public bool Enable { get; set; }

        [MaxWordsExpression(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime CreateTime { get; set; }
    }
}
