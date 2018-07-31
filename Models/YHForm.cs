using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YHSchool.Models
{
    [Table("Ent_YHForm")]
    public class YHForm
    {
        public int ID { get; set; }

        [Required]
        public string FormCode { get; set; }

        [Required]
        public string FormName { get; set; }

        [Required]
        public string MsgTemplate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        public String Creator { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modify Date")]
        public DateTime ModifyDate { get; set; }

        public String Modifier { get; set; }

        public List<Enroll> Enrolls { get; set; }

        /// <summary>
        /// 机器人应用WebHook ID
        /// </summary>
        [Required]
        public int HookID { get; set; }

        [Display(Name = "钉钉Web Hook")]
        public Sysconfig Hook { get; set; }
    }
}
