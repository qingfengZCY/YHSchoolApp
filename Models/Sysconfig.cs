using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YHSchool.Models
{
    [Table("Ref_Sysconfig")]
    public class Sysconfig
    {
        public int ID { get; set; }

        [Display(Name = "配置项 名称")]
        public String ConfigKey { get; set; }

        [Display(Name = "配置项 值")]
        public String ConfigValue { get; set; }

        /// <summary>
        /// 配置项类型 (1, WebHook 2.待确认)
        /// <remark>
        /// </remark>
        /// </summary>
        [Display(Name ="配置项 类型")]
        public int SysType { get; set; }

        [Display(Name = "备注")]
        public String Comments { get; set; }
    }
}
