using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.WeChat
{
    public class WC_OfficalAccountsModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "公众号ID")]
        public string OfficalId { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "公众号名称")]
        public string OfficalName { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "公众号账号")]
        public string OfficalCode { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "头像")]
        public string OfficalPhoto { get; set; }

        [MaxWordsExpression(500)]
        [Display(Name = "EncodingAESKey")]
        public string OfficalKey { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "资源服务器")]
        public string ApiUrl { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "Token")]
        public string Token { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "AppId")]
        public string AppId { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "AppSecret")]
        public string AppSecret { get; set; }

        [MaxWordsExpression(200)]
        [Display(Name = "AccessToken")]
        public string AccessToken { get; set; }

        [MaxWordsExpression(2000)]
        [Display(Name = "说明")]
        public string Remark { get; set; }

        [Display(Name = "是否启用")]
        public bool Enable { get; set; }

        [Display(Name = "是否当前默认操作号")]
        public bool IsDefault { get; set; }

        [Display(Name = "类别（媒体号，企业号，个人号，开发测试号）")]
        public int Category { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "创建人")]
        public string CreateBy { get; set; }

        [Display(Name = "修改时间")]
        public DateTime ModifyTime { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "修改人")]
        public string ModifyBy { get; set; }

    }
}
