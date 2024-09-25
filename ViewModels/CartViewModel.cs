using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using s1411038021_NetFinal.Models;

namespace s1411038021_NetFinal.ViewModels
{
    public class CartViewModel
    {
        public List<Cart> CartList { get; set; }
        public int Sum { get; set; }
        public Cart Cart { get; set; }
    }
}