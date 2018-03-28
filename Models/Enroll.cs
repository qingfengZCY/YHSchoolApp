using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YHSchool.Models
{
    [Table("Ent_Enroll")]
    public class Enroll
    {
        public int ID { get; set; }
        public string FormCode { get; set; }

        [DataType(DataType.MultilineText)]
        /// <summary>
        /// Formated message
        /// </summary>
        public string Message { get; set; }

        [DataType(DataType.MultilineText)]
        /// <summary>
        /// Original Message from YH
        /// </summary>
        public string OriginMsg { get; set; }

        [Display(Name = "Has Send")]
        public bool HasSend { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        public String Creator { get; set; }
    }
}
