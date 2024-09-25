using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace s1411038021_NetFinal.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int bid { get; set; }
        [DisplayName("數量")]
        [Required(ErrorMessage ="請輸入數量")]
        [Range(1,int.MaxValue,ErrorMessage ="數量不可為0")]
        public int amount { get; set; }
        [DisplayName("名字")]
        public string bname { get; set; }
        [DisplayName("價格")]
        public int bprise { get; set; }
        [DisplayName("圖片")]
        public string bfile { get; set; }
        [DisplayName("球皮強度")]
        public string FlareLevel { get; set; }
        public int Flareid { get; set; }
        [DisplayName("品牌")]
        public string BrandName { get; set; }
        public int Brandid { get; set; }
    }
}