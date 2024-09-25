using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using s1411038021_NetFinal.Models;
using s1411038021_NetFinal.ViewModels;
using s1411038021_NetFinal.Services;

namespace s1411038021_NetFinal.Controllers
{
    public class BowlingController : Controller
    {
        public readonly CarNewsDBService CarNewsService = new CarNewsDBService();
        public readonly ballBrandDBService ballService = new ballBrandDBService();
        public readonly CartDBService CartService = new CartDBService();
        // GET: Bowling
        public ActionResult Index()
        {
            CarNewsViewModels Data = new CarNewsViewModels();
            Data.CarList = CarNewsService.GetCarList();
            Data.NewsList = CarNewsService.GetNewsList();
            return View(Data);
        }
        public ActionResult Item(int bid,int brandid,int flareid)
        {
            ballBrandViewModels Data = new ballBrandViewModels();
            /*可以把場牌跟球皮強度都放在ball就好 GetBallById的SQL語法跟廠牌球皮強度JOIN 這樣就不用丟VIEWMODEL到VIEW*/
            Data.ball = ballService.GetBallByBid(bid);
            Data.Brand = ballService.GetBrandById(brandid);
            Data.Flare = ballService.GetFlareById(flareid);
            return View(Data);
        }
        public ActionResult CreateNews()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateNews([Bind(Include = "ntitle")]News Data)
        {
            CarNewsService.InsertNews(Data);
            return RedirectToAction("Index");
        }
        public ActionResult EditNews(string nid)
        {
            News Data = CarNewsService.GetNewByid(nid);
            return View(Data);
        }
        [HttpPost]
        public ActionResult EditNews([Bind(Include = "nid,ntitle")] News Data)
        {
            CarNewsService.EditNew(Data);
            return RedirectToAction("Index");
        }
        public ActionResult CreateBall()
        {
            ballBrandViewModels Data = new ballBrandViewModels();
            Data.BrandList = ballService.GetBrandList();
            Data.FlareList = ballService.GetFlareList();
            return View(Data);
        }
        [HttpPost]
        public ActionResult Createball(int brandid, int flareid, HttpPostedFileBase files,[Bind(Include = "ball")]ballBrandViewModels UpdateData)
        {
            if (files == null) //如果沒圖片重新導回
            {
                TempData["message"] = "請上傳圖片";
                return RedirectToAction("Createball");
            }
            string serverPath = Server.MapPath("~/Images/ballimg/");
            files.SaveAs(serverPath + files.FileName);
            UpdateData.ball.bfile = files.FileName;
            UpdateData.ball.Brandid = brandid;
            UpdateData.ball.Flareid = flareid;
            ballService.CreateBall(UpdateData.ball);
            return RedirectToAction("product", new { Search = UpdateData.ball.bname });
            
        }
        public ActionResult Delete(int nid)
        {
            CarNewsService.DeleteNew(nid);
            return RedirectToAction("Index");
        }
        public ActionResult product(string Search,int Page = 1,int brandid = 0,int flareid = 0)
        {
            ballBrandViewModels Data = new ballBrandViewModels();
            Data.Paging = new ForPaging(Page);
            Data.Search = Search;
            Data = ballService.GetAllBall(brandid,flareid,Data.Paging,Data.Search);
            Data.BrandList = ballService.GetBrandList();
            Data.FlareList = ballService.GetFlareList();
            Data.Flareid = flareid;
            Data.Brandid = brandid;
            return View(Data);
        }
        public ActionResult Editball(int bid,int flareid,int brandid)
        {
            ballBrandViewModels Data = new ballBrandViewModels();
            Data.ball = ballService.GetBallByBid(bid);
            Data.Flareid = flareid;
            Data.Brandid = brandid;
            Data.BrandList = ballService.GetBrandList();
            Data.FlareList = ballService.GetFlareList();
            return View(Data);
        }
        [HttpPost]
        public ActionResult Editball(int bid,int Brandid,int Flareid,HttpPostedFileBase files, [Bind(Include = "ball")]ballBrandViewModels UpdateData)
        {
            UpdateData.ball.Brandid = Brandid;
            UpdateData.ball.Flareid = Flareid;
            UpdateData.ball.bid = bid;
            if(files != null)
            {
                string serverPath = Server.MapPath("~/Images/ballimg/");
                files.SaveAs(serverPath + files.FileName);
                UpdateData.ball.bfile = files.FileName;
            }
            ballService.UpdateBall(UpdateData.ball);
            return RedirectToAction("product", new {Search = UpdateData.ball.bname});
        }
        public ActionResult Deleteball(int bid)
        {
            ballService.Deleteball(bid);
            return RedirectToAction("product");
        }
        [HttpPost]
        public ActionResult CreateCart(int bid)
        {
            CartService.InsertCart(bid);
            return Json((new { message = "成功添加商品到購物車。" }));
        }
        public ActionResult UserEdit()
        {
            ballBrandViewModels Data = new ballBrandViewModels();
            Data.BrandList = ballService.GetBrandList();
            return View(Data);
        }
        public ActionResult CreateNewBrand()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateNewBrand(Brand Data)
        {
            List<Brand> check = ballService.GetBrandList();
            foreach(var item in check)
            {
                if(item.Brandid == Data.Brandid || item.Brandname == Data.Brandname)
                {
                    TempData["message"] = "編號或名稱重複，請重新輸入";
                    return RedirectToAction("UserEdit");
                }
            }
            if(Data.Brandname == "" || Data.Brandid == 0)
            {
                TempData["message"] = "編號不可為0、名稱不可空白，請重新輸入";
                return RedirectToAction("UserEdit");
            }
            ballService.CreateBrand(Data);
            return RedirectToAction("UserEdit");
        }
        public ActionResult DeleteBrand(int Brandid)
        {
            ballService.DeleteBrand(Brandid);
            return RedirectToAction("UserEdit");
        }
        public ActionResult EditBrand(int Brandid)
        {
            Brand Data = ballService.GetBrandById(Brandid);
            return View(Data);
        }
        
        [HttpPost]
        public ActionResult EditBrand(Brand UpData)
        {
            ballService.UpdateBrand(UpData);
            return RedirectToAction("UserEdit");
        }
    }
}