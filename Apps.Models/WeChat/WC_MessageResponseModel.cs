using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.WeChat
{
    public class WC_MessageResponseModel
    {
        [MaxWordsExpression(50)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [MaxWordsExpression(50)]
        [Display(Name = "所属公众号")]
        public string OfficalAccountId { get; set; }

        [Display(Name = "消息规则（枚举）")]
        public int? MessageRule { get; set; }

        [Display(Name = "类型（枚举）")]
        public int? Category { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "关键字")]
        public string MatchKey { get; set; }

        [MaxWordsExpression(-1)]
        [Display(Name = "文本内容")]
        public string TextContent { get; set; }

        [MaxWordsExpression(-1)]
        [Display(Name = "图文文本内容")]
        public string ImgTextContext { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "图文图片URL")]
        public string ImgTextUrl { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "图文图片超链接")]
        public string ImgTextLink { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "语音URL")]
        public string MeidaUrl { get; set; }

        [MaxWordsExpression(1000)]
        [Display(Name = "语音超链接")]
        public string MeidaLink { get; set; }

        [Display(Name = "是否启用")]
        public bool Enable { get; set; }

        [Display(Name = "是否默认")]
        public bool IsDefault { get; set; }

        [MaxWordsExpression(2000)]
        [Display(Name = "说明")]
        public string Remark { get; set; }

        [Display(Name = "排序")]
        public int Sort { get; set; }

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

        public enum WeChatReplyCategory
        {
            //文本
            Text = 1,
            //图文
            Image = 2,
            //语音
            Voice = 3,
            //相等，用于回复关键字
            Equal = 4,
            //包含，用于回复关键字
            Contain = 5
        }

        public enum WeChatRequestRuleEnum
        {
            /// <summary>
            /// 默认回复，没有处理的
            /// </summary>
            Default = 0,
            /// <summary>
            /// 关注回复
            /// </summary>
            Subscriber = 1,
            /// <summary>
            /// 文本回复
            /// </summary>
            Text = 2,
            /// <summary>
            /// 图片回复
            /// </summary>
            Image = 3,
            /// <summary>
            /// 语音回复
            /// </summary>
            Voice = 4,
            /// <summary>
            /// 视频回复
            /// </summary>
            Video = 5,
            /// <summary>
            /// 超链接回复
            /// </summary>
            Link = 6,
            /// <summary>
            /// LBS位置回复
            /// </summary>
            Location = 7,
        }
    }
}
