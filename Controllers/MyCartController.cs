using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using s1411038021_NetFinal.Models;
using s1411038021_NetFinal.Services;
using s1411038021_NetFinal.ViewModels;

namespace s1411038021_NetFinal.Controllers
{
    public class MyCartController : Controller
    {
        CartDBService CartService = new CartDBService();
        // GET: MyCart
        public ActionResult Index()
        {
            CartViewModel DataList = new CartViewModel();
            DataList.CartList = CartService.GetAllCart();
            DataList.Sum = CartService.GetSum();
            return View(DataList);
        }
        public ActionResult Delete(int CartId)
        {
            CartService.Delete(CartId);
            return RedirectToAction("Index");
        }
        public ActionResult EditCart(int CartId)
        {
            Cart Data = CartService.GetCartByCartId(CartId);
            return PartialView(Data);
        }
        [HttpPost]
        public ActionResult EditCart(int CartId,[Bind(Include ="amount")]Cart UpdateData)
        {
            UpdateData.CartId = CartId;
            if(UpdateData.amount>0)
            {
                CartService.Updateamount(UpdateData);
            }
            else
            {
                TempData["message"] = "數量不可為0或少於0";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}