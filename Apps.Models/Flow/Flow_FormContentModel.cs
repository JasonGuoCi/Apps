using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Flow
{
    public class Flow_FormContentModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "发起用户")]
        public string UserId { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "对应表单")]
        public string FormId { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "公文级别")]
        public string FormLevel { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrA")]
        public string AttrA { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrB")]
        public string AttrB { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrC")]
        public string AttrC { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrD")]
        public string AttrD { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrE")]
        public string AttrE { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrF")]
        public string AttrF { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrG")]
        public string AttrG { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrH")]
        public string AttrH { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrI")]
        public string AttrI { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrJ")]
        public string AttrJ { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrK")]
        public string AttrK { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrL")]
        public string AttrL { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrM")]
        public string AttrM { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrN")]
        public string AttrN { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrO")]
        public string AttrO { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrP")]
        public string AttrP { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrQ")]
        public string AttrQ { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrR")]
        public string AttrR { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrS")]
        public string AttrS { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrT")]
        public string AttrT { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrU")]
        public string AttrU { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrV")]
        public string AttrV { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrW")]
        public string AttrW { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrX")]
        public string AttrX { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrY")]
        public string AttrY { get; set; }

        [MaxWordsExpression(2048)]
        [Display(Name = "AttrZ")]
        public string AttrZ { get; set; }

        [MaxWordsExpression(4000)]
        [Display(Name = "CustomMember")]
        public string CustomMember { get; set; }

        [Display(Name = "TimeOut")]
        public DateTime TimeOut { get; set; }
    }
}
