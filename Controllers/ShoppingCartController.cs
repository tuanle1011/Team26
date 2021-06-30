using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComTam_Store.Models;
namespace ComTam_Store.Controllers
{
    public class ShoppingCartController : Controller
    {
        CT25Team26Entities db = new CT25Team26Entities();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart; 
        }
        public ActionResult AddtoCart(int id)
        {
            var pro = db.Products.SingleOrDefault(x => x.IdSP == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult ShowCart()
        {          
                if (Session["Cart"] == null)
                    return RedirectToAction("ShowCart", "ShoppingCart");
                Cart cart = Session["Cart"] as Cart;
                return View(cart);   
        }
        public ActionResult Update_Quantity (FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro =int.Parse( form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity(id_pro, quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart (int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveCartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int totalitem = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            
                totalitem = cart.TotalProInCart();
                ViewBag.Quantity = totalitem;
                return PartialView("BagCart");
              
        }
        public ActionResult Shopping_Suc()
        {
            return View();
        }
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Cart info = new Cart();
                HoaDon hoa = new HoaDon
                {
                    TenKh = info.TenKh,
                    SDT = info.SDT,
                    Diachi = info.Diachi,
                    NgayTao = DateTime.Now
                };
                db.HoaDons.Add(hoa);
                db.SaveChanges();
                foreach( var item in cart.Items)
                {
                    ChitietHD chitiet = new ChitietHD();
                    chitiet.MaHD = hoa.MaHD;
                    chitiet.MaSP = item.Product.IdSP;
                    chitiet.Gia = item.Product.Gia;
                    chitiet.SoLuong = item.Product.SoLuong;
                    db.ChitietHDs.Add(chitiet);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return Content("Error");
            }
        }
        
    }
}