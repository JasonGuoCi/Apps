using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_SealModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "Path")]
        public string Path { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        [MaxWordsExpression(4000)]
        [Display(Name = "使用者")]
        public string Using { get; set; }
    }
}
