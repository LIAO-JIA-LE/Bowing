using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using s1411038021_NetFinal.Models;
using s1411038021_NetFinal.Services;

namespace s1411038021_NetFinal.ViewModels
{
    public class ballBrandViewModels
    {
        public Brand Brand { get; set; }
        public Flare Flare { get; set; }
        public Bowlingballs ball { get; set; }
        public List<Brand> BrandList { get; set; }
        public List<Bowlingballs> ballList { get; set; }
        public List<Flare> FlareList { get; set; }
        public int Flareid { get; internal set; }
        public int Brandid { get; internal set; }
        public ForPaging Paging { get; set; }
        public string Search { get; set; }
    }
}