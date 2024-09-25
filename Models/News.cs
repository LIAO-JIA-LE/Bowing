using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s1411038021_NetFinal.Models
{
    public class News
    {
        public string nid { get; set; }
        [DisplayName("標題")]
        [Required(ErrorMessage ="請輸入標題")]
        [StringLength(30,ErrorMessage ="標題不可超過30字")]
        public string ntitle { get; set; }
        [DisplayName("最後更新時間")]
        public DateTime ntime { get; set; }
    }
}