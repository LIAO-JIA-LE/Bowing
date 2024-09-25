using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s1411038021_NetFinal.Models
{
    public class Brand
    {
        [DisplayName("品牌編號")]
        [Required(ErrorMessage = "編號不可空白")]
        [Range(1,int.MaxValue,ErrorMessage ="編號不可為0")]
        public int Brandid { get; set; }
        [DisplayName("品牌名稱")]
        [Required(ErrorMessage ="名稱不可空白")]
        [MaxLength(20,ErrorMessage ="不可超過20字元")]
        public string Brandname { get; set; }
    }
}