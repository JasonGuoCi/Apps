using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.MIS
{
    public class MIS_Article_CategoryModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "ChannelId")]
        public int? ChannelId { get; set; }

        [MaxWordsExpression(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "ParentId")]
        public string ParentId { get; set; }

        [Display(Name = "Sort")]
        public int? Sort { get; set; }

        [MaxWordsExpression(255)]
        [Display(Name = "ImgUrl")]
        public string ImgUrl { get; set; }

        [MaxWordsExpression(8000)]
        [Display(Name = "BodyContent")]
        public string BodyContent { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "Enable")]
        public bool Enable { get; set; }
    }
}
