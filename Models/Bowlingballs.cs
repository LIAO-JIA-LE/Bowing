using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace s1411038021_NetFinal.Models
{
    public class Bowlingballs
    {
        [DisplayName("產品編號")]
        public int bid { get; set; }
        [DisplayName("產品名稱")]
        [Required(ErrorMessage ="請輸入名稱")]
        [StringLength(20,ErrorMessage ="名稱不可超過20字")]
        public string bname { get; set; }
        [DisplayName("產品價格")]
        [Required(ErrorMessage = "請輸入價格")]
        public int bprise { get; set; }
        public string bfile { get; set; }
        [DisplayName("產品介紹")]
        public string bcontent { get; set; }
        [DisplayName("發布時間")]
        public DateTime btime { get; set; }
        [DisplayName("廠牌")]
        public int Brandid { get; set; }
        [DisplayName("球皮強度")]
        public int Flareid { get; set; }
    }
}