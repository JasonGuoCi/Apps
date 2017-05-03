using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class SysUserModel
    {

        [Display(Name = "Id")]
        public string Id { get; set; }


        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Display(Name = "Password")]
        public string Password { get; set; }


        [Display(Name = "TrueName")]
        public string TrueName { get; set; }


        [Display(Name = "Card")]
        public string Card { get; set; }


        [Display(Name = "身份证")]
        public string MobileNumber { get; set; }


        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }


        [Display(Name = "QQ")]
        public string QQ { get; set; }


        [Display(Name = "EmailAddress")]
        public string EmailAddress { get; set; }


        [Display(Name = "OtherContact")]
        public string OtherContact { get; set; }


        [Display(Name = "Province")]
        public string Province { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Village")]
        public string Village { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "State")]
        public bool? State { get; set; }

        [Display(Name = "CreateTime")]
        public DateTime? CreateTime { get; set; }


        [Display(Name = "CreatePerson")]
        public string CreatePerson { get; set; }


        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "JoinDate")]
        public DateTime? JoinDate { get; set; }

        [Display(Name = "婚姻")]
        public string Marital { get; set; }


        [Display(Name = "党派")]
        public string Political { get; set; }


        [Display(Name = "民族")]
        public string Nationality { get; set; }


        [Display(Name = "籍贯")]
        public string Native { get; set; }


        [Display(Name = "毕业学校")]
        public string School { get; set; }


        [Display(Name = "就读专业")]
        public string Professional { get; set; }


        [Display(Name = "学历")]
        public string Degree { get; set; }


        [Display(Name = "部门")]
        public string DepId { get; set; }


        [Display(Name = "职位")]
        public string PosId { get; set; }


        [Display(Name = "个人简介")]
        public string Expertise { get; set; }


        [Display(Name = "在职状况")]
        public string JobState { get; set; }


        [Display(Name = "照片")]
        public string Photo { get; set; }


        [Display(Name = "附件")]
        public string Attach { get; set; }

        [Display(Name = "Flag")]
        public string Flag { get; set; }


        [MaxWordsExpression(4000)]
        [Display(Name = "上级领导")]
        public string Lead { get; set; }

        [MaxWordsExpression(4000)]
        [Display(Name = "上级领导")]
        public string LeadName { get; set; }

        [Display(Name = "是否可以自选领导")]
        public bool IsSelLead { get; set; }

        [Display(Name = "日否启动日程汇报是否启用  启用后 他的上司领导将可以看到他的 工作日程汇报.")]
        public bool IsReportCalendar { get; set; }

        [Display(Name = "开启 小秘书消息提示(默认每10分钟提示一次)开启 小秘书消息提示(每2分钟提示一次)开启 小秘书消息提示(每5分钟提示一次)开启 小秘书消息提示(每20分钟提示一次)开启 小秘书消息提示(每30分钟提示一次)开启 小秘书消息提示(每1小时提示一次)开启 小秘书消息提示(每2小时提示一次)禁用 小秘书消息提示(不再提示)")]
        public bool IsSecretary { get; set; }
    }
}
