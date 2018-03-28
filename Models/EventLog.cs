using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YHSchool.Models
{
    [Table("Zzz_EventLog")]
    public class EventLog
    {
        public int ID { get; set; }

        public int EnrollID { get; set; }
        public String ActionType { get; set; }

        /// <summary>
        /// Log Level (0: Info 1: Error 2. System)
        /// </summary>
        public string LogLevel { get; set; }
        public String Comments { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        public String Creator { get; set; }
    }
}
