using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s1411038021_NetFinal.Models
{
    public class Flare
    {
        public int Flareid { get; set; }
        [DisplayName("球皮強度")]
        [Required(ErrorMessage = "不可空白")]
        [MaxLength(20, ErrorMessage = "不可超過20字元")]
        public string FlareLevel { get; set; }
    }
}