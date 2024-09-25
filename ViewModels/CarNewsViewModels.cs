using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using s1411038021_NetFinal.Models;
using s1411038021_NetFinal.Services;

namespace s1411038021_NetFinal.ViewModels
{
    public class CarNewsViewModels
    {
        public List<Carousel> CarList { get; set; }
        public List<News> NewsList { get; set; }
    }
}