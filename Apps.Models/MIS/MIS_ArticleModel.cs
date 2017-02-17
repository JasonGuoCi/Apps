using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.MIS
{
    public class MIS_ArticleModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "ChannelId")]
        public int ChannelId { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "CategoryId")]
        public string CategoryId { get; set; }

        [MaxWordsExpression(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [MaxWordsExpression(255)]
        [Display(Name = "ImgUrl")]
        public string ImgUrl { get; set; }

        [MaxWordsExpression(8000)]
        [Display(Name = "BodyContent")]
        public string BodyContent { get; set; }

        [Display(Name = "Sort")]
        public int? Sort { get; set; }

        [Display(Name = "Click")]
        public int? Click { get; set; }

        [Display(Name = "CheckFlag")]
        public int CheckFlag { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "Checker")]
        public string Checker { get; set; }

        [Display(Name = "CheckDateTime")]
        public DateTime? CheckDateTime { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "Creater")]
        public string Creater { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }
    }
}
